using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Text;

namespace DI
{
    class Class4
    {
        public interface IAccount { }
        public interface IMessage { }
        public interface ITool { }

        public interface ITest
        {
            public IMessage Message { get; set; }
        }


        public class Base
        {
            public Base()
            {
                Console.WriteLine($"Created:{GetType().Name}");
            }

        }

        public class Account : Base, IAccount { }
        public class Message : Base, IMessage { }

        public class Tool : Base, ITool { }

        public class Test : ITest
        {
            public IMessage Message { get; set; }

            public Test(IAccount account, ITool tool)
            {
                Console.WriteLine($"Ctor:Test(IAccount, ITool)");
            }
        }
        public static void Run()
        {
            var serviceCollection = new ServiceCollection()
                .AddTransient<ITool, Tool>();

            var containerBuilder = new ContainerBuilder();
            containerBuilder.Populate(serviceCollection);

            containerBuilder.RegisterType<Test>().As<ITest>().PropertiesAutowired();

            containerBuilder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .Where(t => t.BaseType == typeof(Base))
                .As(t => t.GetInterfaces()[0])
                .InstancePerLifetimeScope();

            var container = containerBuilder.Build();
            var provider = new AutofacServiceProvider(container);

            Debug.Assert(provider.GetService<IAccount>() is Account);
            Debug.Assert(provider.GetService<IMessage>() is Message);
            Debug.Assert(provider.GetService<ITool>() is Tool);

            var test = provider.GetService<ITest>();
            Debug.Assert(test.Message is Message);
            Console.Read();
        }
    }
}
