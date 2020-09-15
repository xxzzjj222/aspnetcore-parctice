using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using Microsoft.Extensions.Logging.Debug;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    public class Class2
    {
        public static void Run()
        {
            var provierService = new ServiceCollection()
                .AddLogging(builder =>
                {
                    builder.AddFilter((provider,category, level) =>
                    {
                        if (provider==typeof(ConsoleLoggerProvider).FullName)
                        {
                            return level >= LogLevel.Error;
                        }
                        if (provider == typeof(DebugLoggerProvider).FullName)
                        {
                            return level >= LogLevel.Warning;
                        }
                        return category switch
                        {
                            "LoggerA" => level >= LogLevel.Debug,
                            "LoggerB" => level >= LogLevel.Warning,
                            "LoggerC" => level >= LogLevel.None,
                            _ => level >= LogLevel.Information
                        };
                    })
                    .AddConsole()
                    .AddDebug();
                })
                .BuildServiceProvider();

            var loggerFactory = provierService.GetRequiredService<ILoggerFactory>();
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
