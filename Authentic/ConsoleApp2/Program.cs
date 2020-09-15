using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {
            Host.CreateDefaultBuilder()
                .ConfigureWebHostDefaults(builder => builder
                    .UseUrls("http://*:9097")
                    .Configure(app => app
                    .Run(ProcessAsync)))
                .Build()
                .Run();
        }

        public static async Task ProcessAsync(HttpContext context)
        {
            context.Response.ContentType = "text/html";

            var html =
                @"<html>
                <body>
                    <ul id='contacts'></ul>
                    <script src='http://code.jquery.com/jquery-3.3.1.min.js'></script>
                    <script>
                    $(function()
                    {
                        var url = 'http://www.one.com:9099/contacts';
                        $.getJSON(url, null, function(contacts) {
                            $.each(contacts, function(index, contact)
                            {
                                var html = '<li><ul>';
                                html += '<li>Name: ' + contact.Name + '</li>';
                                html += '<li>Phone No:' + contact.PhoneNo + '</li>';
                                html += '<li>Email Address: ' + contact.EmailAddress + '</li>';
                                html += '</ul>';
                                $('#contacts').append($(html));
                            });
                        });
                    });
                    </script >
                </body>
                </html>";

            await context.Response.WriteAsync(html);
        }
    }
}
