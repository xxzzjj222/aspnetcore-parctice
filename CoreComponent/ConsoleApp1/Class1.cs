using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class Class1
    {
        public static void Run()
        {
            Host.CreateDefaultBuilder()
                .ConfigureWebHostDefaults(builder =>
                {
                    builder.Configure(app =>
                    {
                        //app.Run(context =>
                        //{
                        //    return Task.FromException(new InvalidOperationException());
                        //});
                        app.UseDeveloperExceptionPage();
                        app.UseRouting();
                        app.UseEndpoints(route =>
                        {
                            route.MapGet("/", HandleAsync);
                        });
                    })
                    .ConfigureServices(services =>
                    {
                        services.AddRouting();
                    });
                })
                .Build()
                .Run();
        }

        private static Task HandleAsync(HttpContext context)
        {
            return Task.FromException(new InvalidOperationException("This is Exception"));
        }

    }
}
