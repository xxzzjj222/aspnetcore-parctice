using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using System;
using System.Collections.Generic;
using System.Security.Authentication.ExtendedProtection;
using System.Text;

namespace FileAndCOnfig
{
    class Class1
    {
        public static void Run()
        {
            var provider = new ServiceCollection()
                .AddSingleton<IFileProvider>(new PhysicalFileProvider(@"G:\\xzj"))
                .AddSingleton<FileManager>()
                .BuildServiceProvider();

            var fileManager = provider.GetService<FileManager>();
            fileManager.Dir();
        }

        public class FileManager
        {
            private readonly IFileProvider _fileProvider;

            public FileManager(IFileProvider fileProvider)
            {
                _fileProvider = fileProvider;
            }

            public void Dir()
            {
                var indent = -1;

                void Get(string subpath)
                {
                    indent++;
                    foreach (var fileInfo in _fileProvider.GetDirectoryContents(subpath))
                    {
                        Console.WriteLine(new string('\t',indent)+fileInfo.Name);
                        if (fileInfo.IsDirectory)
                        {
                            Get($@"{subpath}\{fileInfo.Name}");
                        }
                    }
                }

                Get("");
            }
        }
    }
}
