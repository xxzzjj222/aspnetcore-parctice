using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    public class Class3
    {
        public class AppConfigDemo
        {
            public string Name { get; set; }
            public string StartDate { get; set; }
            public string EndDate { get; set; }
        }

        public static void Run()
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("muti.json", false, true)
                .Build();

            var provider = new ServiceCollection()
                .Configure<AppConfigDemo>(configuration.GetSection("DefaultApp"))
                .Configure<AppConfigDemo>(configuration.GetSection("CustomApp"))
                .BuildServiceProvider();

            //var provider = new ServiceCollection()
            //    .Configure<AppConfigDemo>(config =>
            //    {
            //        config.Name = "aaa";
            //        config.StartDate = "2020";
            //        config.EndDate = "2021";
            //    })
            //    .BuildServiceProvider();



            var options = provider.GetRequiredService<IOptionsMonitor<AppConfigDemo>>();
            Console.WriteLine(options.CurrentValue.Name);


            options.OnChange((config, configName) =>
            {
                Console.WriteLine(config.Name);
                Console.WriteLine(configName);
            });
        }
    }
}
