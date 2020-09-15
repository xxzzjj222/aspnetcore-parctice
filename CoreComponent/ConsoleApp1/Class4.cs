using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class Class4
    {
        private static readonly Random random = new Random();
        public static void Run()
        {
            Host.CreateDefaultBuilder()
                .ConfigureWebHostDefaults(builder =>
                {
                    builder.Configure(app =>
                    {
                        //app.UseStatusCodePages(Handler)
                        //app.UseStatusCodePagesWithRedirects("/error/{0}")
                        app.UseStatusCodePagesWithReExecute("/error/{0}")
                        .UseRouting()
                        .UseEndpoints(route=>route.MapGet("/error/{status_code}", Handler1))
                        .Run(context => Task.Run(()=>context.Response.StatusCode=random.Next(400,600)));
                    })
                    .ConfigureServices(services=>services.AddRouting());
                })
                .Build()
                .Run();
        }

        public static async Task Handler(StatusCodeContext context)
        {
            if (context.HttpContext.Response.StatusCode>500)
            {
                await context.HttpContext.Response.WriteAsync($"ServerError{context.HttpContext.Response.StatusCode}");
            }
            else
            {
                await context.HttpContext.Response.WriteAsync($"ClientrError{context.HttpContext.Response.StatusCode}");
            }
        }

        public static async Task Handler1(HttpContext context)
        {
            var code = context.GetRouteData().Values["status_code"];
            await context.Response.WriteAsync($"Error ({code})");
        }
    }
}
