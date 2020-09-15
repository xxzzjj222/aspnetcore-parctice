using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class WeatherReport
    {
        private static readonly string[] Conditions = { "晴", "多云", "小雨" };
        private static readonly Random Random = new Random();

        public string City { get; }

        public IDictionary<DateTime,WeatherInfo> WeatherInfos { get; }

        public class WeatherInfo
        {
            /// <summary>
            /// 天气状况
            /// </summary>
            public string Condition { get; set; }

            /// <summary>   
            /// 最高温度
            /// </summary>
            public double HighTemperature { get; set; }

            /// <summary>
            /// 最低温度
            /// </summary>
            public double LowTemperature { get; set; }
        }

        public WeatherReport(string city,int days)
        {
            City = city;
            WeatherInfos = new Dictionary<DateTime, WeatherInfo>();
            for (var i = 0; i < days; i++)
                WeatherInfos[DateTime.Today.AddDays(i + 1)] = new WeatherInfo
                {
                    Condition = Conditions[Random.Next(0, 2)],
                    HighTemperature = Random.Next(20, 30),
                    LowTemperature = Random.Next(10, 20)
                };
        }
    }
}
