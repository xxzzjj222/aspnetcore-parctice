using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace ConsoleApp2
{
    public class Class1
    {
        public static void Run()
        {
            var serviceProvider = new ServiceCollection()
                .AddLogging(builder =>
                {
                    builder
                    .AddConsole()
                    //.AddDebug();
                    .AddTraceSource(new SourceSwitch("Class1", nameof(SourceLevels.All)), new DefaultTraceListener { LogFileName = "trace.log" })
                    .SetMinimumLevel(LogLevel.Trace);
                }).BuildServiceProvider();

            var logger = serviceProvider.GetRequiredService<ILoggerFactory>().CreateLogger("Class1");
            var levels = (LogLevel[])Enum.GetValues(typeof(LogLevel));
            var eventId = 1;
            for (int i = 0; i < levels.Length; i++)
            {
                logger.Log(levels[i], eventId++, $"{levels[i]}信息");
            }
        }
    }
}
