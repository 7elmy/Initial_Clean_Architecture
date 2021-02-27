using Initial_Clean_Architecture.Ioc.Dependencies;
using Initial_Clean_Architecture.Ioc.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Initial_Clean_Architecture.Ioc.Extensions
{
    public static class DependencyContainerServiceExtension
    {
        /// <summary>
        /// regist all dependencies depend on their order
        /// </summary>
        /// <param name="services"></param>
        public static void RegistAllDependencies(this IServiceCollection services)
        {
            var registers = Assembly.GetExecutingAssembly().ExportedTypes.Where(x =>
             typeof(IRegister).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract).Select(
               Activator.CreateInstance).Cast<IRegister>().ToList();

            registers.ForEach(register => register.RegisterServices(services));
        }

        public static void RegistDataDependencies(this IServiceCollection services)
        {
            var dataDependencies = new DataDependencies();
            dataDependencies.RegisterServices(services);
        }

        public static void RegistApplicationDependencies(this IServiceCollection services)
        {
            var applicationDependencies = new ApplicationDependencies();
            applicationDependencies.RegisterServices(services);
        }

    }
}
