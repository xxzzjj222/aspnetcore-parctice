using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static ConsoleApp1.Class4;

namespace ConsoleApp1
{
    public class Class4
    {
        public class Test1 { }
        public class Test2 { }

        public class Startup
        {
            public void Configure(IApplicationBuilder app)
            {

            }

            public void ConfigureServices(IServiceCollection services)
            {

            }
        }

        public static void Start()
        {
            var host = Host.CreateDefaultBuilder()
                .ConfigureWebHostDefaults(builder =>
                {
                    builder.ConfigureServices(collection =>
                    {
                        collection.AddSingleton<Test1>()
                        .AddSingleton<Test2>()
                        .AddControllersWithViews();
                    })
                    .Configure(app =>
                    {
                        app.UseRouting()
                        .UseEndpoints(routeBuilder =>
                        {
                            routeBuilder.MapControllers();
                        });
                    });
                })
                .Build();

            host.Run();
        }
    }

    public class HomeController : Controller
    {
        private readonly Test1 _test1;

        public HomeController(Test1 test1)
        {
            _test1 = test1;
        }

        [HttpGet("/")]
        public IActionResult Index()
        {
            ViewBag.Test1 = _test1;
            return View();
        }
    }
}
