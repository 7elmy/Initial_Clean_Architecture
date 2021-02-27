using Initial_Clean_Architecture.Data.Contexts;
using Initial_Clean_Architecture.Helpers.ServicesInstallers.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Initial_Clean_Architecture.API.ServicesInstallers
{
    public class DBServiceInstaller : IServiceInstaller
    {
        public void InstallService(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContextPool<AppDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("AppConnection")));
        }
    }
}
