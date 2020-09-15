using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace DI
{
    class Class2
    {
        public interface IAccount { }
        public interface IMessage { }
        public interface ITool { }

        public interface ITest { }

        public class Account : IAccount { }
        public class Message : IMessage { }
        public class Tool : ITool { }
        public class Test : ITest
        {
            // 如果某个构造函数的参数类型集合，能够称为所有合法构造函数参数类型集合的超集
            // 那个这个构造函数就会被服务提供对象所选择。
            public Test(IAccount account)
            {
                Console.WriteLine($"Ctor:Test(IAccount)");
            }

            public Test(IAccount account, IMessage message)
            {
                Console.WriteLine($"Ctor:Test(IAccount,IMessage)");
            }

            public Test(IAccount account, IMessage message, ITool tool)
            {
                Console.WriteLine($"Ctor:Test(IAccount,IMessage,ITool)");
            }
        }

        public static void Run()
        {
            var test = new ServiceCollection()
                .AddTransient<IAccount, Account>()
                .AddTransient<IMessage, Message>()
                .AddTransient<ITool, Tool>()
                .AddTransient<ITest, Test>()
                .BuildServiceProvider()
                .GetService<ITest>();
        }
    }
}
