using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FileAndCOnfig
{
    class Class2
    {
        public static void Run()
        {
            var provider = new ServiceCollection()
                .AddSingleton<IFileProvider>(new EmbeddedFileProvider(Assembly.GetExecutingAssembly()))
                .AddTransient<FileManager>()
                .BuildServiceProvider();

            var fileManager = provider.GetService<FileManager>();
            var content=fileManager.ReadAsync("test.txt").Result;
            Console.WriteLine(content);
        }

        public class FileManager
        {
            private readonly IFileProvider _fileProvider;
            public FileManager(IFileProvider fileProvider)
            {
                _fileProvider = fileProvider;
            }

            public async Task<string> ReadAsync(string path)
            {
                await using var stream = _fileProvider.GetFileInfo(path).CreateReadStream();
                byte[] buffer = new byte[stream.Length];
                await stream.ReadAsync(buffer, 0, buffer.Length);
                return Encoding.UTF8.GetString(buffer);
            }
        }
    }
}
