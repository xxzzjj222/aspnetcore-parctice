using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    class Class1
    {
        public class AppConfigDemo
        {
            public string Name { get; set; }
            public string StartDate { get; set; }
            public string EndDate { get; set; }

            public AppConfigDemo(IConfiguration config)
            {
                Name = config["Name"];
                StartDate = config["StartDate"];
                EndDate = config["EndDate"];
            }
        }

        public static void Run()
        {
            var source = new Dictionary<string, string>
            {
                ["Name"] = "AppConfig",
                ["StratDate"] = "202000",
                ["EndData"] = "202010"
            };

            var config = new ConfigurationBuilder()
                .AddInMemoryCollection(source)
                .Build();

            var options = new AppConfigDemo(config);
            Console.WriteLine(options.Name);

        }
    }
}
