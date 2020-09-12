using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    public class Class1
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
                .AddJsonFile("muti.json")
                .Build();

            var provider = new ServiceCollection()
                .Configure<AppConfigDemo>(configuration.GetSection("DefaultApp"))
                .Configure<AppConfigDemo>(configuration.GetSection("CustomApp"))
                .BuildServiceProvider();

            var options = provider.GetRequiredService<IOptionsSnapshot<AppConfigDemo>>();

            var defaultapp = options.Get("DefaultApp");
            var customapp = options.Get("CustomApp");

            Console.WriteLine(defaultapp.Name);
            Console.WriteLine(customapp.Name);
        }
    }
}
