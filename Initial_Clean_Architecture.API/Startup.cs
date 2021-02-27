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
            services.InstallServices(_configuration, Assembly.GetExecutingAssembly());
            services.RegistAllDependencies();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint(_swaggerSettings.UIEndpoint, _swaggerSettings.Title));
            }


            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

    }
}
