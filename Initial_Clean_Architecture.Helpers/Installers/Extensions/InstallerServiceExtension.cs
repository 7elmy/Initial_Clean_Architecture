using Initial_Clean_Architecture.Helpers.Installers.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Initial_Clean_Architecture.Helpers.Installers.Extensions
{
    public static class InstallerServiceExtension
    {
        public static void InstallServices(this IServiceCollection services, IConfiguration configuration, Assembly assembly)
        {
            var installers = assembly.ExportedTypes.Where(x =>
              typeof(IServicesInstaller).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract).Select(
                Activator.CreateInstance).Cast<IServicesInstaller>().ToList();

            installers.ForEach(installer => installer.InstallServices(services, configuration));
        }
    }
}
