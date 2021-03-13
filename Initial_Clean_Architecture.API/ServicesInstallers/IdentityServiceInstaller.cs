using Initial_Clean_Architecture.Data.Contexts;
using Initial_Clean_Architecture.Data.Domain.Entities;
using Initial_Clean_Architecture.Helpers.ServicesInstallers.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Initial_Clean_Architecture.API.ServicesInstallers
{
    public class IdentityServiceInstaller : IServiceInstaller
    {

        public void InstallService(IServiceCollection services, IConfiguration configuration)
        {
            services.AddIdentity<AppUser, IdentityRole>(options =>
            {
                //overide passowrd options
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredUniqueChars = 0;
            }).AddEntityFrameworkStores<AppDbContext>();
        }
    }
}
