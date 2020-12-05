using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Initial_Clean_Architecture.Ioc.Interfaces
{
    interface IRegister
    {
        void RegisterServices(IServiceCollection services);
        int Order { get; set; }
    }
}
