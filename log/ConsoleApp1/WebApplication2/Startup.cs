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

namespace WebApplication2
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,ILogger<Startup> logger)
        {
            var scopeFactory = LoggerMessage.DefineScope<Guid>("Request Tran {Guid}");
            var requestLog = LoggerMessage.Define<string, DateTime>(LogLevel.Information, 0, "Request {state} at {DateTime.Now}");

            app.Use(async (context, next) =>
            {
                using (scopeFactory(logger, Guid.NewGuid()))
                {
                    requestLog(logger, "Begin", DateTime.Now, null);
                    await next();
                    requestLog(logger, "End", DateTime.Now, null);
                }
            });

            app.Run(async context =>
            {
                await Task.Delay(1000);
                await context.Response.WriteAsync("Hello");
            });
        }
    }
}
