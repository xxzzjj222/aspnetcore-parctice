using Microsoft.Extensions.DependencyInjection;
using System;

namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {
            //Class4.Run();
            var services = new ServiceCollection();
            foreach (var item in services)
            {
                Console.WriteLine(item.ServiceType);
            }

            Console.Read();
        }
    }
}
