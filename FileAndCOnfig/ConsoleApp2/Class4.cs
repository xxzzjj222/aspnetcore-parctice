using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    public class Class4
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
                .Build();


            var services = new ServiceCollection();
            services
                .AddOptions<AppConfigDemo>()
                .Configure(options =>
                {
                    options.Name = config["Name"] ?? "";
                    options.Version = config["Version"] ?? "";
                })
                .Validate(demo => demo.Name.StartsWith("a"),"姓名参数无效")
                .Validate(demo => demo.Version.StartsWith("b"),"版本参数无效");

            try
            {
                var options = services.BuildServiceProvider()
                    .GetRequiredService<IOptions<AppConfigDemo>>()
                    .Value;

                Console.WriteLine(options.Name);
                Console.WriteLine(options.Version);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
