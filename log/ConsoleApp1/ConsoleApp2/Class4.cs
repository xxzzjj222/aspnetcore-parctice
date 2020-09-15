using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    public class Class4
    {
        public static void Run()
        {
            var serviceProvider = new ServiceCollection()
                .AddLogging(builder =>
                {
                    builder.AddConsole()
                    .SetMinimumLevel(LogLevel.Information);
                })
                .AddSingleton<GodLog>()
                .BuildServiceProvider();

            var logger = serviceProvider.GetRequiredService<GodLog>();
            logger.Log("Hello");
            Console.Read();
        }

        public class GodLog
        {
            private const string Template = "[{LogTime}]来自{Id}-{Name}的记录：{content}";
            private static Action<ILogger, DateTime, int, string, string, Exception> _logAction;
            private readonly ILogger _logger;

            public GodLog(ILogger<Class4> logger)
            {
                _logger = logger;
                _logAction = LoggerMessage.Define<DateTime,int,string,string>(LogLevel.Information, 0, Template);
            }

            public void Log(string content)
            {
                _logAction(_logger, DateTime.Now, 1, "God", content, null);
            }
        }
    }
}
