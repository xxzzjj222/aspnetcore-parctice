using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace DI
{
    class Class1
    {
        public interface IAccount { }
        public interface IMessage { }
        public interface ITool { }

        public class Base:IDisposable
        {
            public Base()
            {
                Console.WriteLine($"Created:{GetType().Name}");
            }

            public void Dispose()
            {
                Console.WriteLine($"Dispose:{GetType().Name}");
            }
        }

        public class Account : Base,IAccount
        { }

        public class Message : Base, IMessage
        { }

        public class Tool : Base, ITool
        {
        }

        public static void Run()
        {

            using (var root = new ServiceCollection()
                 .AddTransient<IAccount, Account>()
                 .AddScoped<IMessage, Message>()
                 .AddSingleton<ITool, Tool>()
                 .BuildServiceProvider())
            {
                using (var scope=root.CreateScope())
                {
                    var child = scope.ServiceProvider;
                    child.GetService<IAccount>();
                    child.GetService<IMessage>();
                    child.GetService<ITool>();
                    Console.WriteLine("释放子容器");
                }
                Console.WriteLine("释放跟容器");
            }
            

            //var provider = new ServiceCollection()
            //    .AddTransient<IAccount, Account>()
            //    .AddScoped<IMessage, Message>()
            //    .AddSingleton<ITool, Tool>()
            //    .BuildServiceProvider();

            //var child1 = provider.CreateScope().ServiceProvider;
            //var child2 = provider.CreateScope().ServiceProvider;

            //GetService<IAccount>(child1);
            //GetService<IMessage>(child1);
            //GetService<ITool>(child1);
            //Console.WriteLine();
            //GetService<IAccount>(child2);
            //GetService<IMessage>(child2);
            //GetService<ITool>(child2);


            //Debug.Assert(provider.GetService<IAccount>() is Account);
            //Debug.Assert(provider.GetService<IMessage>() is Message);
            //Debug.Assert(provider.GetService<ITool>() is Tool);

            //Debug.Assert(provider.OfType<Account>().Any());
            //Debug.Assert(provider.OfType<Message>().Any());
            //Debug.Assert(provider.OfType<Tool>().Any());
        }

        public static void GetService<T>(IServiceProvider provider)
        {
            provider.GetService<T>();
            provider.GetService<T>();
        }
    }
}
