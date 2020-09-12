using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    class Class4
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
                .AddJsonFile("appsetting.json",true,true)
                .Build();

            var options = config.Get<AppConfigDemo>();
            Read(options);

            ChangeToken.OnChange(() => config.GetReloadToken(), () =>
               {
                   Read(config.Get<AppConfigDemo>());
               });
        }

        public static void Read(AppConfigDemo options)
        {
            Console.Clear();
            Console.WriteLine($"Name:{options.Name}");
            Console.WriteLine($"StartDate:{options.StartDate}");
            Console.WriteLine($"EndDate:{options.EndDate}");
            Console.WriteLine($"Behavior.IsRead:{options.Behavior.IsRead}");
            Console.WriteLine($"Behavior.IsWrite:{options.Behavior.IsWrite}");
        }

    }
}
