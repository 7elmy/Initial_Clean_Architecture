using Initial_Clean_Architecture.Data.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Initial_Clean_Architecture.Data.Domain.DbConfigs.Extensions
{
    public static class DbConfigExtension
    {
        /// <summary>
        /// install all db config for any class implements "IDbConfig"
        /// </summary>
        /// <param name="modelBuilder"></param>
        public static void InstallDbConfig(this ModelBuilder modelBuilder)
        {
            var installers = Assembly.GetExecutingAssembly().ExportedTypes.Where(x =>
              typeof(IDbConfig).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract).Select(
                Activator.CreateInstance).Cast<IDbConfig>().ToList();

            installers.ForEach(installer => installer.InstallDbConfig(modelBuilder));
        }
    }
}
