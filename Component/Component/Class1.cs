using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Component
{
    public class Class1
    {
        public interface ITemperatureCollector
        {
            int Get();
        }

        public interface IHumidityCollector
        {
            int Get();
        }

        public interface IAirQualityCollector
        {
            int Get();
        }

        public class Collector : ITemperatureCollector, IHumidityCollector, IAirQualityCollector
        {
            public Collector()
            {
                Console.WriteLine($"Collector构造");
            }
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

        public class AirEnviromentOptions
        {
            public long Interval {  get; set; }
        }

        public class AirEnvironmentService : IHostedService
        {

            private readonly ITemperatureCollector _temperatureCollector;
            private readonly IHumidityCollector _humidityCollector;
            private readonly IAirQualityCollector _airQualityCollector;
            private readonly AirEnviromentOptions _options;

            private Timer _timer;

            public AirEnvironmentService(
                ITemperatureCollector temperatureCollector,
                IHumidityCollector humidityCollector,
                IAirQualityCollector airQualityCollector,
                IOptions<AirEnviromentOptions> options
            )
            {
                _temperatureCollector = temperatureCollector;
                _humidityCollector = humidityCollector;
                _airQualityCollector = airQualityCollector;
                _options = options.Value;
            }
            public Task StartAsync(CancellationToken cancellationToken)
            {
                _timer = new Timer(state =>
                  {
                      Console.WriteLine($"温度:{_temperatureCollector.Get(),-10}\n湿度:{_humidityCollector.Get(),-10}\n空气质量:{_airQualityCollector.Get(),-10}");
                  }, null, 0, _options.Interval);
                return Task.CompletedTask;
            }

            public Task StopAsync(CancellationToken cancellationToken)
            {
                _timer?.Dispose();
                return Task.CompletedTask;
            }
        }

        public static void Start(string[] args)
        {
            var collector= new Collector();

            var host = new HostBuilder()
                .ConfigureHostConfiguration(builder =>
                {
                    builder.AddCommandLine(args);
                })
                .ConfigureAppConfiguration((context,builder)=>
                {
                    builder.AddJsonFile("appsetting.json", false,true);
                    builder.AddJsonFile($"appsetting.{context.HostingEnvironment.EnvironmentName}.json", false, true);
                })
                .ConfigureServices((context,collection) =>
                {
                    collection.AddHostedService<AirEnvironmentService>()
                    .AddSingleton<ITemperatureCollector>(collector)
                    .AddSingleton<IHumidityCollector>(collector)
                    .AddSingleton<IAirQualityCollector>(collector)
                    .AddOptions()
                    .Configure<AirEnviromentOptions>(context.Configuration.GetSection("AirEnvironment"));
                    
                    //.BuildServiceProvider();

                })
                .Build();

            host.Run();
        }
    }
}
