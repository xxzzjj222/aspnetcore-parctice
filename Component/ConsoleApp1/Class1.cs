using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class Class1
    {
        public class CustomMiddle : IMiddleware
        {
            public Task InvokeAsync(HttpContext context, RequestDelegate next)
            {
                context.Response.WriteAsync("aaaa");
                return Task.CompletedTask;
            }
        }

        public static void Start()
        {
            var host = Host.CreateDefaultBuilder()
                .ConfigureServices(collection =>
                {
                    collection.AddSingleton<CustomMiddle>();
                })
                .ConfigureWebHost((builder) =>
                {
                    builder.UseKestrel();
                    builder.Configure(builder =>
                    {
                        builder.UseMiddleware<CustomMiddle>();
                    });
                })
                .Build();

            host.Run();
        }
    }
}
