using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class Class
    {
        public static readonly Dictionary<string, string> Cities = new Dictionary<string, string>()
        {
            ["010"] = "北京",
            ["020"] = "杭州"
        };

        public static void Run()
        {
            // const string template = @"weather/{city:regex(^{0\d{{2,3}}$)}/{days:int:range(1,4)}"; 
            const string template = @"weather/{city=010}/{days=5}";
            Host.CreateDefaultBuilder()
                .ConfigureWebHost(builder =>
                {
                    builder.UseKestrel();
                    builder.ConfigureServices(collection =>
                    {
                        collection.AddRouting();
                    })
                    .Configure(app =>
                    {
                        app.UseRouting();
                        app.UseEndpoints(endpoint =>
                        {
                            endpoint.MapGet(template, WeatherForecase);
                        });
                    });
                })
                .Build()
                .Run();
        }

        public static async Task WeatherForecase(HttpContext context)
        {
            var city = (string)context.GetRouteData().Values["city"];
            city = Cities[city];
            var days = int.Parse(context.GetRouteData().Values["days"].ToString() ?? string.Empty);
            var report = new WeatherReport(city, days);
            await RendWeatherAsync(context, report);
        }

        public static async Task RendWeatherAsync(HttpContext context,WeatherReport report)
        {
            context.Response.ContentType = "text/html;charset=utf-8";
            await context.Response.WriteAsync("<html><head><title>天气</title></head><body>");
            await context.Response.WriteAsync($"<h3>{report.City}</h3>");
            foreach (var (key,value) in report.WeatherInfos)
            {
                await context.Response.WriteAsync($"{key:yyyy-MM-dd}:");
                await context.Response.WriteAsync(
                    $"{value.Condition}({value.LowTemperature}℃ ~ {value.HighTemperature}℃)<br/><br/> ");
            }
            await context.Response.WriteAsync("</body></html>");
        }
    }
}
