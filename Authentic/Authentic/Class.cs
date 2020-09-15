using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Authentic
{
    public class Class
    {
        private static readonly Dictionary<string, string> Accounts = new Dictionary<string, string>
        {
            //["admin"]="123",
            { "aaa","123"},
            { "bbb", "123" },

        };
        public static void Start()
        {
            Host.CreateDefaultBuilder()
                .ConfigureWebHostDefaults(builder => builder
                    .ConfigureServices(services => services
                        .AddRouting()
                        .AddAuthentication(options => options
                            .DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme)
                        .AddCookie())
                    .Configure(app => app
                        .UseRouting()
                        .UseAuthentication()
                        .UseEndpoints(endpoints =>
                        {
                            endpoints.Map("/", RenderHomePageAsync);
                            endpoints.Map("Account/Login", SignInAsync);
                            endpoints.Map("Account/Logout", SignOutAsync);
                        })))
                .Build()
                .Run();
        }

        public static async Task RenderHomePageAsync(HttpContext context)
        {
            if (context?.User?.Identity?.IsAuthenticated==true)
            {
                await context.Response.WriteAsync(
                    @"<html>
                    <head><title>Index</title></head>
                    <body>" +
                    $"<h3>Welcome {context.User.Identity.Name}</h3>" +
                    @"<a href='/Account/Logout'>Sign Out</a>
                    </body>
                </html>");
            }
            else
            {
                await context.ChallengeAsync();
            }
        }

        public static async Task SignInAsync(HttpContext context)
        {
            if (string.CompareOrdinal(context.Request.Method,"GET")==0)
            {
                await RenderLoginPageAsync(context, null, null, null);
            }
            else
            {
                var userName = context.Request.Form["username"];
                var password = context.Request.Form["password"];
                if (Accounts.TryGetValue(userName,out var pwd) &&pwd== password)
                {
                    var identity = new GenericIdentity(userName, "Password");
                    var principal = new ClaimsPrincipal(identity);
                    await context.SignInAsync(principal);
                }
                else
                {
                    await RenderLoginPageAsync(context, null, null, "用户名密码无效");
                }
            }
        }

        private static  Task RenderLoginPageAsync(HttpContext context,string userName,string password,string errorMessage)
        {
            context.Response.ContentType = "text/html";
            return  context.Response.WriteAsync(
                @"<html>
                <head><title>Login</title></head>
                <body>
                    <form method='post'>" +
                $"<input type='text' name='username' placeholder='User name' value = '{userName}' /> " +
                $"<input type='password' name='password' placeholder='Password' value = '{password}' /> " +
                @"<input type='submit' value='Sign In' />
                    </form>" +
                $"<p style='color:red'>{errorMessage}</p>" +
                @"</body>
            </html>");
        }

        public static async Task SignOutAsync(HttpContext context)
        {
            await context.SignOutAsync();
            context.Response.Redirect("/");
        }
    }
}
