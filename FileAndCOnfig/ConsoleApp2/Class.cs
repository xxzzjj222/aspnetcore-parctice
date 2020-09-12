using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    public class Class
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
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsetting.json")
                .Build();

            var provider = new ServiceCollection()
                //.AddOptions()
                .Configure<AppConfigDemo>(config)
                .BuildServiceProvider();

            var options = provider.GetRequiredService<IOptions<AppConfigDemo>>();
            var appconfig = options.Value;

            Console.WriteLine(appconfig.Name);
            Console.WriteLine(appconfig.StartDate);
        }
    }
}
