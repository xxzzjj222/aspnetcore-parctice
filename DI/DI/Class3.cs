using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace DI
{
    class Class3
    {
        public interface IAccount { }
        public interface IMessage { }
        public interface ITool { }

        public interface ITest { }

        public class Account : IAccount { }
        public class Message : IMessage { }
        public class Tool : ITool { }

        public static void Run()
        {
            var serviceCollection = new ServiceCollection()
                .AddTransient<IAccount, Account>()
                .AddTransient<IMessage, Message>();

            var containerBuilder = new ContainerBuilder();
            containerBuilder.Populate(serviceCollection);
            containerBuilder.RegisterType<Tool>().As<ITool>();

            var container = containerBuilder.Build();
            var provider = new AutofacServiceProvider(container);

            provider.GetService<IAccount>();

        }
    }
}
