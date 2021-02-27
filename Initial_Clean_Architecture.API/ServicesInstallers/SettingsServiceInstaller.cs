using Initial_Clean_Architecture.Application.Domain.Settings;
using Initial_Clean_Architecture.Helpers.ServicesInstallers.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Initial_Clean_Architecture.API.ServicesInstallers
{
    public class SettingsServiceInstaller : IServiceInstaller
    {
        public void InstallService(IServiceCollection services, IConfiguration configuration)
        {
            //  services.Configure<LoggingSettings>(options => configuration.GetSection(nameof(LoggingSettings)).Bind(options));
        }
    }
}
