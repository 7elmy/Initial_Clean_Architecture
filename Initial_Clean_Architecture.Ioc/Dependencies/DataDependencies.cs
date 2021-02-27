using Initial_Clean_Architecture.Data.Contexts;
using Initial_Clean_Architecture.Data.Domain.Interfaces;
using Initial_Clean_Architecture.Data.UnitOfWork;
using Initial_Clean_Architecture.Ioc.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Initial_Clean_Architecture.Ioc.Dependencies
{
    public class DataDependencies : IRegister
    {

        public void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork<AppDbContext>>();
        }
    }
}
