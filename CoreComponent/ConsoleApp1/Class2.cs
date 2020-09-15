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
    public class Class2
    {
        public static void Run()
        {
            var options = new ExceptionHandlerOptions()
            {
                ExceptionHandler = context => context.Response.WriteAsync("Hello Exception")
            };

            Host.CreateDefaultBuilder()
                .ConfigureWebHostDefaults(builder =>
                {
                    builder.Configure(app => app.UseExceptionHandler(appBuilder =>
                    {
                        appBuilder.Run(context => context.Response.WriteAsync("Hello Exception"));
                    })
                    //.UseRouting()
                    //.UseEndpoints(points => points.MapGet("/", context => Task.FromException(new InvalidOperationException("Throw Exception")))));
                    .Run(context => Task.FromException(new InvalidOperationException("Throw Exception"))));
                })
                .ConfigureServices(services=>services.AddRouting())
                .Build()
                .Run();
        }

    }
}
