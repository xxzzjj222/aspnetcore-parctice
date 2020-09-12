using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Component
{
    public class Class
    {
        public class SystemClock : IHostedService
        {
            private Timer _timer;
            public Task StartAsync(CancellationToken cancellationToken)
            {
                _timer = new Timer(state =>
                  {
                      Console.WriteLine($"currenttime:{DateTime.Now.ToLongTimeString()}");
                  }, null, 0, 1000);
                return Task.CompletedTask;
            }

            public Task StopAsync(CancellationToken cancellationToken)
            {
                _timer?.Dispose();
                return Task.CompletedTask;
            }
        }

        public static void Start()
        {
            var host = new HostBuilder()
                .ConfigureServices(collection =>
                {
                    collection.AddSingleton<IHostedService, SystemClock>();
                })
                .Build();

            host.Run();
        }
    }
}
