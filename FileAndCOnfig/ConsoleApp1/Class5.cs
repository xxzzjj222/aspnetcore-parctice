using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    class Class5
    {
        public class AppConfigDemo
        {
            public string Name { get; set; }
            public string Ver { get; set; }
        }

        public static void Run()
        {
            Environment.SetEnvironmentVariable("APP_NAME", "AppDemo");
            Environment.SetEnvironmentVariable("APP_VER", "Alpha");

            var configuration = new ConfigurationBuilder()
                .AddEnvironmentVariables("APP_")
                .Build()
                .Get<AppConfigDemo>();

            Console.WriteLine(configuration.Name);
        }
    }
}
