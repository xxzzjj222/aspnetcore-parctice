using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Component
{
    public class Class2
    {
        public interface ITemperatureCollector
        {
            int Get();
        }

        /// <summary>
        /// 湿度
        /// </summary>
        public interface IHumidityCollector
        {
            int Get();
        }

        /// <summary>
        /// 空气质量
        /// </summary>
        public interface IAirQualityCollector
        {
            int Get();
        }


        public class Collector : ITemperatureCollector, IHumidityCollector, IAirQualityCollector
        {
            int ITemperatureCollector.Get()
            {
                var random = new Random();
                return random.Next(0, 100);
            }

            int IHumidityCollector.Get()
            {
                var random = new Random();
                return random.Next(0, 100);
            }

            int IAirQualityCollector.Get()
            {
                var random = new Random();
                return random.Next(0, 100);
            }
        }

        public class AirEnvironmentOptions
        {
            public long Interval { get; set; }
        }

        public class AirEnvironmentPublisher
        {
            private const string Template= "温度：{temperature, -10}" +
                                             "湿度：{humidity, -10}" +
                                             "空气质量：{airQuality, -10}" +
                                             "时间:{now}";
            private readonly Action<ILogger, int, int, int, string, Exception> _logAction;
            private readonly ILogger _logger;

            public AirEnvironmentPublisher(ILogger<AirEnvironmentPublisher> logger)
            {
                _logger = logger;
                _logAction = LoggerMessage.Define<int, int, int, string>(LogLevel.Information, 0, Template);
            }

            public void Publish(int temp, int humi, int airq)
            {
                _logAction(_logger,temp,humi,airq,DateTime.Now.ToLongTimeString(),null);
            }
        }
        public class AirEnvironmentService : IHostedService
        {
            private readonly ITemperatureCollector _temperatureCollector;
            private readonly IHumidityCollector _humidityCollector;
            private readonly IAirQualityCollector _airQualityCollector;
            private readonly AirEnvironmentPublisher _publisher;
            private readonly AirEnvironmentOptions _options;

            private Timer _timer;

            public AirEnvironmentService(
                ITemperatureCollector temperatureCollector,
                IHumidityCollector humidityCollector,
                IAirQualityCollector airQualityCollector,
                AirEnvironmentPublisher publisher,
                IOptions<AirEnvironmentOptions> options
            )
            {
                _temperatureCollector = temperatureCollector;
                _humidityCollector = humidityCollector;
                _airQualityCollector = airQualityCollector;
                _publisher = publisher;
                _options = options.Value;
            }

            public Task StartAsync(CancellationToken cancellationToken)
            {
                _timer = new Timer(state =>
                {
                    _publisher.Publish(_temperatureCollector.Get(), _humidityCollector.Get(), _airQualityCollector.Get());
                }, null, 0, _options.Interval);

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
            var collector = new Collector();

            var host = new HostBuilder()
                .ConfigureAppConfiguration((context, builder) => builder
                    .AddJsonFile("appsetting.json",false))
                    //.AddJsonFile($"appsetting.{context.HostingEnvironment.EnvironmentName}.json",false))
                .ConfigureServices((context, builder) =>
                {
                    builder.AddHostedService<AirEnvironmentService>()
                    .AddSingleton<ITemperatureCollector>(collector)
                    .AddSingleton<IHumidityCollector>(collector)
                    .AddSingleton<IAirQualityCollector>(collector)
                    .AddSingleton<AirEnvironmentPublisher>()
                    //.AddOptions()
                    .Configure<AirEnvironmentOptions>(context.Configuration.GetSection("AirEnvironment"));
                })
                .ConfigureLogging((context, builder) =>
                {
                    builder.AddConfiguration(context.Configuration.GetSection("Logging"));
                    builder.AddConsole();
                })
                .Build();

            host.Run();
        }
    }
}
