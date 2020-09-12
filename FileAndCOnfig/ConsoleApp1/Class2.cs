using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Memory;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    class Class2
    {
        public class AppConfigDemo
        {
            public string Name { get; set; }
            public string StartDate { get; set; }
            public string EndDate { get; set; }
        }

        public static void Run()
        {
            var source = new Dictionary<string, string>
            {
                ["Name"] = "AppConfig",
                ["StratDate"] = "202000",
                ["EndData"] = "202010"
            };

            var options = new ConfigurationBuilder()
                //.AddInMemoryCollection(source)
                .Add(new MemoryConfigurationSource() { InitialData = source })
                .Build()
                .Get<AppConfigDemo>();

            Console.WriteLine(options.Name);

        }
    }
}
