using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CoreComponent
{
    class Class1
    {
        public static void Run()
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "content");

            var options = new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(path),
                RequestPath = "/aaa"
            };

            var directoryBrowserOptions = new DirectoryBrowserOptions()
            {
                FileProvider = new PhysicalFileProvider(path),
                RequestPath = "/aaa"
            };


            var defaultFileOptions = new DefaultFilesOptions()
            {
                FileProvider = new PhysicalFileProvider(path),
                RequestPath = "/aaa"
            };
            defaultFileOptions.DefaultFileNames.Add("readme.html");

            var host=Host.CreateDefaultBuilder()
                .ConfigureWebHostDefaults(builder=>
                {
                    builder.Configure(app =>
                    {
                        app.UseDefaultFiles();
                        app.UseDefaultFiles(defaultFileOptions);
                        app.UseStaticFiles();
                        app.UseStaticFiles(options);
                        app.UseDirectoryBrowser();
                        app.UseDirectoryBrowser(directoryBrowserOptions);
                    });
                })
                .Build();
            host.Run();
                
        }
    }
}
