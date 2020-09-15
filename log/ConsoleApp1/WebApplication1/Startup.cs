using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace WebApplication1
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddLogging(builder =>
            {
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
        {
            app.Use(async (context, next) =>
            {
                using (logger.BeginScope($"Request Trans {Guid.NewGuid()}"))
                {
                    logger.Log(LogLevel.Information, $"Request Begin at {DateTime.Now}");
                    await next();
                    logger.Log(LogLevel.Information, $"Request End at {DateTime.Now}");
                }
            });

            app.Run(async (context)=> 
            {
                await Task.Delay(1000);
                await context.Response.WriteAsync("Hello");
            });
        }
    }
}
