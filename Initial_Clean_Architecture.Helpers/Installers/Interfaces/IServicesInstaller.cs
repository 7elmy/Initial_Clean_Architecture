using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Initial_Clean_Architecture.Helpers.Installers.Interfaces
{
    public interface IServicesInstaller
    {
        void InstallServices(IServiceCollection services, IConfiguration configuration);
    }
}
