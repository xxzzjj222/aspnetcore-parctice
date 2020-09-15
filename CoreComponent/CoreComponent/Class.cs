using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreComponent
{
    public class Class
    {
        public static void Run()
        {
            var contentTypeProvider = new FileExtensionContentTypeProvider();
            contentTypeProvider.Mappings.Add(".abc", "images/jpg");

            var options = new StaticFileOptions
            {
                //ServeUnknownFileTypes = true,
                // DefaultContentType = "image/jpg"
                ContentTypeProvider = contentTypeProvider
            };

            Host.CreateDefaultBuilder()
                .ConfigureWebHostDefaults(builder => builder.Configure(
                    app => app
                        .UseStaticFiles(options)
                        .UseDirectoryBrowser()))
                .Build()
                .Run();
        }
    }
}
