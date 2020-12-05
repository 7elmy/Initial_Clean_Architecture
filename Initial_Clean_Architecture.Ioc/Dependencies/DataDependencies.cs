using Initial_Clean_Architecture.Ioc.Constants;
using Initial_Clean_Architecture.Ioc.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Initial_Clean_Architecture.Ioc.Dependencies
{
    public class DataDependencies : IRegister
    {
        public int Order { get; set; } = (int)DependenciesOrderEnum.ApplicationDependencies;

        public void RegisterServices(IServiceCollection services)
        {

        }
    }
}
