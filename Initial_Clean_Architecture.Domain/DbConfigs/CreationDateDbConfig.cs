using Initial_Clean_Architecture.Data.Domain.Constants;
using Initial_Clean_Architecture.Data.Domain.Entities;
using Initial_Clean_Architecture.Data.Domain.Entities.Common;
using Initial_Clean_Architecture.Data.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Initial_Clean_Architecture.Data.Domain.DbConfigs
{
    public class CreationDateDbConfig : IDbConfig
    {
        public void InstallDbConfig(ModelBuilder modelBuilder)
        {
            var entitiesTypes = Assembly.GetExecutingAssembly().ExportedTypes.Where(x =>
             typeof(ICreationDate).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract).ToList();

            entitiesTypes.ForEach(entityType => modelBuilder.Entity(entityType)
                 .Property(nameof(ICreationDate.CreationDate))
                 .HasColumnType(SQLConst.SmallDateTimeType)
                 .HasDefaultValueSql(SQLConst.CurrentUTCDateFunction)
                 );
        }
    }
}
