using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    class Class6
    {
        public class AppConfigDemo
        {
            public string Name { get; set; }
            public string Version { get; set; }
        }

        public static void Run(string[] args)
        {
            var mapping = new Dictionary<string, string>()
            {
                ["-n"] = "Name",
                ["-ver"] = "Version"
            };

            var config = new ConfigurationBuilder()
                .AddCommandLine(args, mapping)
                .Build()
                .Get<AppConfigDemo>();

            Console.WriteLine(config.Name);
            Console.WriteLine(config.Version);
        }
    }
}
