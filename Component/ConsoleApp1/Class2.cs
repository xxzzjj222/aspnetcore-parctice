using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class Class2
    {
        public class CustomMiddle
        {
            private readonly RequestDelegate _next;
            private readonly string _content;
            private readonly bool _isToNext;


            public CustomMiddle(RequestDelegate next, string content, bool isToNext = false)
            {
                _next = next;
                _content = content;
                _isToNext = isToNext;
            }

            public async Task InvokeAsync(HttpContext context)
            {
                await context.Response.WriteAsync($"hello:{_content}:begin\n");
                if (_isToNext)
                {
                    await _next(context);
                }
                await context.Response.WriteAsync($"hello:{_content}:end\n");
            }
        }

        public static void Start()
        {
            var host = Host.CreateDefaultBuilder()
                .ConfigureWebHost(builder =>
                {
                    builder.UseKestrel();
                    builder.Configure(app =>
                    {
                        app.UseMiddleware<CustomMiddle>("aaa", true)
                        .UseMiddleware<CustomMiddle>("bbb", true)
                        .UseMiddleware<CustomMiddle>("ccc");
                    });
                })
                .Build();

            host.Run();
        }
    }
}
