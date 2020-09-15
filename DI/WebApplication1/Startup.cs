using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebApplication1.Controllers;
using static WebApplication1.Services;

namespace WebApplication1
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddControllers().AddControllersAsServices();
            //services.AddTransient<IAccount, Account>();
            //services.AddTransient<IMessage, Message>();
            //services.AddAutofac();

            services.AddControllers();
            services.Scan(scan =>
            {
                scan.FromAssemblyOf<Startup>()
                .AddClasses(classes =>
                {
                    classes.Where(t => t.Name.EndsWith("Service", StringComparison.OrdinalIgnoreCase));
                })
                .AsImplementedInterfaces()
                .AsSelf()
                .WithScopedLifetime();
            });
        }


        public void ConfigureContainer(ContainerBuilder builder)
        {
            //var assembly = Assembly.GetEntryAssembly();

            //builder.RegisterAssemblyTypes(assembly)
            //    .Where(t => t.BaseType == typeof(Account))
            //    .As(t=>t.GetInterfaces()[0])
            //    .InstancePerLifetimeScope();

            //builder.RegisterType<MessageController>().PropertiesAutowired();

            //builder.RegisterAssemblyTypes(assembly)
            //    .Where(t => typeof(ControllerBase).IsAssignableFrom(t) && t != typeof(ControllerBase))
            //    .InstancePerLifetimeScope()
            //    .PropertiesAutowired();

            //builder.RegisterModule<ControllerModule>();
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                //endpoints.MapGet("/", async context =>
                //{
                //    await context.Response.WriteAsync("Hello World!");
                //});
                endpoints.MapControllers();
            });
        }
    }
}
