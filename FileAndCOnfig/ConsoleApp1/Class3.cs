using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    class Class3
    {
        public class AppConfigDemo
        { 
            public string Name { get; set; }
            public string StartData { get; set; }
            public string EndData { get; set; }

            public Behavior Behavior { get; set; }

        }

        public class Behavior
        {
            public bool IsRead { get; set; }
            public bool IsWrite { get; set; }
        }

        public static void Run()
        {
            var options = new ConfigurationBuilder()
                //.Add(new JsonConfigurationSource() { Path= "appsetting.json" })
                .AddJsonFile("appsetting.json")
                .Build()
                .Get<AppConfigDemo>();

            Console.WriteLine($"{options.Name}|{options.Behavior.IsRead}|");
        }
    }
}
