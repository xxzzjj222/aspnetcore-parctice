using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    class Class7
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
                .AddXmlFile("app.config")
                .Build()
                .GetSection(nameof(AppConfigDemo))
                .Get<AppConfigDemo>();

            Console.WriteLine(config.Name);
        }
    }
}
