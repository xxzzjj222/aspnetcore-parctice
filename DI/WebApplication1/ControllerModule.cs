using Autofac;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using WebApplication1.Controllers;

namespace WebApplication1
{
    public class ControllerModule:Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var assembly = Assembly.GetEntryAssembly();

            //builder.RegisterAssemblyTypes(assembly)
            //    .Where(t => t.BaseType == typeof(Account))
            //    .As(t=>t.GetInterfaces()[0])
            //    .InstancePerLifetimeScope();

            builder.RegisterType<MessageController>().PropertiesAutowired();

            //builder.RegisterAssemblyTypes(assembly)
            //    .Where(t => typeof(ControllerBase).IsAssignableFrom(t) && t != typeof(ControllerBase))
            //    .InstancePerLifetimeScope()
            //    .PropertiesAutowired();
        }
    }
}
