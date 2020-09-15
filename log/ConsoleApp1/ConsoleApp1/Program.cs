using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Extensions.Logging;
using NLog.Web;
using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
           // Class4.Run();

            var services = new ServiceCollection().AddLogging(config=> {
                config.AddNLog();
            }).BuildServiceProvider();

            var logger = services.GetRequiredService<ILogger<Program>>();
            logger.LogError("错误");

            Console.Read();
        }
    }
}
