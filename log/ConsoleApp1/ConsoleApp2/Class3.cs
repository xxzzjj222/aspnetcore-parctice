using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    public class Class3
    {
        public static void Run()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("log.json")
                .Build();

            var serviceProvider = new ServiceCollection()
                .AddLogging(builder =>
                {
                    builder.AddConfiguration(config)
                    .AddConsole()
                    .AddDebug();
                }).BuildServiceProvider();

            var loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();
            var loggerA = loggerFactory.CreateLogger("LoggerA");
            var loggerB = loggerFactory.CreateLogger("LoggerB");
            var loggerC = loggerFactory.CreateLogger("LoggerC");

            var leveles = (LogLevel[])Enum.GetValues(typeof(LogLevel));

            var eventId = 1;
            foreach (var level in leveles)
            {
                eventId++;
                loggerA.Log(level, eventId, $"这是一条{level}消息");
                loggerB.Log(level, eventId, $"这是一条{level}消息");
                loggerC.Log(level, eventId, $"这是一条{level}消息");
            }
        }
    }
}
