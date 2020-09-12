using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class Class
    {
        public static void Start()
        {
            var host = Host.CreateDefaultBuilder()
                .ConfigureWebHost(builder =>
                {
                    builder.UseKestrel()
                    .Configure(app =>
                    {
                        //app.Run(context => context.Response.WriteAsync("asdfafa"));
                        app.Use(MiddleWare1)
                        .Use(MiddleWare2);
                    });
                })
                .Build();

            host.Run();
        }

        public static RequestDelegate MiddleWare1(RequestDelegate next)
        {
           async Task App(HttpContext context)
            {
                await context.Response.WriteAsync("Middleware 1 Begin.");
                await next(context);
                await context.Response.WriteAsync("Middleware 1 End.");
            }
            return App;
        }

        public static RequestDelegate MiddleWare2(RequestDelegate next)
        {
            async Task App(HttpContext context)
            {
                await context.Response.WriteAsync("Middleware 2 Begin.");
            }
            return App;

        }


        //public static async Task App(HttpContext context)
        //{
        //    await context.Response.WriteAsync("Middleware 2 Begin.");
        //}
    }
}
