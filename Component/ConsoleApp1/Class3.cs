using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class Class3
    {
        public class StartUp
        {
            public void ConfigureServices(IServiceCollection serviceCollection)
            {
                foreach (var service in serviceCollection)
                {
                    var serviceName = service.ServiceType.Name;
                    var implType = service.ImplementationType;
                    if (implType!=null)
                    {
                        Console.WriteLine($"{service.Lifetime,-15}{serviceName,-40}{implType.Name}");
                    }
                }
            }

            public void Configure(IApplicationBuilder app)
            { 
            }
        }

        public static void Start()
        {
            var host = Host.CreateDefaultBuilder()
                .ConfigureWebHostDefaults(builder =>
                {
                    builder.UseStartup<StartUp>();
                })
                .Build();

            host.Run();
        }
    }
}
