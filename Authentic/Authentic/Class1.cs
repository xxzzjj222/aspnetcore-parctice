using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Session;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Authentic
{
    class Class1
    {
        public static void Start()
        {
            Host.CreateDefaultBuilder()
                .ConfigureWebHostDefaults(builder => builder
                    .ConfigureServices(services => services
                        .AddDistributedMemoryCache()
                        .AddSession())
                    .Configure(app => app.UseSession()
                    .Run(Handler)))
                .Build()
                .Run();
        }

        public static async Task Handler(HttpContext context)
        {
            var session = context.Session;
            await session.LoadAsync();
            string sessionStartTime;

            if (session.TryGetValue("SessionStartTime",out var value))
            {
                sessionStartTime = Encoding.UTF8.GetString(value);                
            }
            else
            {
                sessionStartTime = DateTime.Now.ToString(CultureInfo.InvariantCulture);
                session.Set("SessionStartTime", Encoding.UTF8.GetBytes(sessionStartTime));
            }

            var field = typeof(DistributedSession).GetTypeInfo().GetField("_sessionKey", BindingFlags.Instance | BindingFlags.NonPublic);
            var sessionKey = field?.GetValue(session); 

            context.Response.ContentType = "text/html";
            await context.Response.WriteAsync($"<html><body><ul><li>Session ID:{session.Id}</li>");
            await context.Response.WriteAsync($"<li>Session Key:{sessionKey}</li>");
            await context.Response.WriteAsync($"<li>Session Start Time:{sessionStartTime}</li>");
            await context.Response.WriteAsync($"<li>Current Time:{DateTime.Now}</li></ul></table></body></html>");
        }
    }
}
