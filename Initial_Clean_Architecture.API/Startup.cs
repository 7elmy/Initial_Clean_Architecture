using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Initial_Clean_Architecture.Helpers.ServicesInstallers.Extensions;
using Initial_Clean_Architecture.Application.Domain.Settings;
using Initial_Clean_Architecture.Ioc.Extensions;
using Initial_Clean_Architecture.API.Extensions;
using Microsoft.AspNetCore.Identity;
using Initial_Clean_Architecture.Data.Domain.Entities;
using Initial_Clean_Architecture.Application.Domain.Seeds;
using Microsoft.Extensions.Options;
using Initial_Clean_Architecture.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Initial_Clean_Architecture.API
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        private readonly SwaggerSettings _swaggerSettings;
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
            _swaggerSettings = new SwaggerSettings();
            configuration.GetSection(_swaggerSettings.GetType().Name).Bind(_swaggerSettings);
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.RegistAllDependencies();
            services.InstallServices(_configuration, Assembly.GetExecutingAssembly());
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,
            UserManager<AppUser> userManager,
            RoleManager<IdentityRole> roleManager, AppDbContext dataContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                dataContext.Database.Migrate();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint(_swaggerSettings.UIEndpoint, _swaggerSettings.Title));

            SeedData(userManager, roleManager);

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseExceptions();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private void SeedData(UserManager<AppUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            RolesSeed.Seed(roleManager);
            SeedUsers(userManager);
        }

        private void SeedUsers(UserManager<AppUser> userManager)
        {
            var superAdminSettings = new SuperAdminSettings();
            _configuration.GetSection(nameof(SuperAdminSettings)).Bind(superAdminSettings);
            UsersSeed.Seed(userManager, superAdminSettings);
        }
    }
}
