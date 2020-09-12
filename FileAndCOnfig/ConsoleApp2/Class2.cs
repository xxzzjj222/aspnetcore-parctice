using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    public class Class2
    {
        public class AppConfigDemo
        {
            public string Name { get; set; }
            public string StartDate { get; set; }
            public string EndDate { get; set; }

            public Behavior Behavior { get; set; }
        }

        public class Behavior
        {
            public bool IsRead { get; set; }
            public bool IsWrite { get; set; }
        }

        public static void Run()
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsetting.json", false, true)
                .Build();

            var serviceProvider = new ServiceCollection()
                .Configure<AppConfigDemo>(configuration)
                .BuildServiceProvider();

            var options = serviceProvider.GetRequiredService<IOptionsMonitor<AppConfigDemo>>();
            options.OnChange(config =>
            {
                Console.WriteLine(config.Name);
            });
        }
    }
}
