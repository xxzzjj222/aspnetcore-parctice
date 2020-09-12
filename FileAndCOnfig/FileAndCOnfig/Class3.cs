using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FileAndCOnfig
{
    class Class3
    {
        public static void Run()
        {
            var provider = new ServiceCollection()
                .AddSingleton<IFileProvider>(new PhysicalFileProvider(@"G:\\test"))
                .AddTransient<FileManager>()
                .BuildServiceProvider();

            var fileManager = provider.GetService<FileManager>();
            fileManager.WatchAsync("test.txt").Wait();
        }

        public class FileManager
        {
            private readonly IFileProvider _fileProvider;
            public FileManager(IFileProvider fileProvider)
            {
                _fileProvider = fileProvider;
            }

            public async Task WatchAsync(string path)
            {
                Console.WriteLine(await ReadAsync(path));

                ChangeToken.OnChange(() => _fileProvider.Watch(path), async () =>
                   {
                       Console.Clear();
                       Console.WriteLine(await ReadAsync(path));
                   });
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
