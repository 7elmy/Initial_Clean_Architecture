using Initial_Clean_Architecture.Application.Domain.Settings;
using Initial_Clean_Architecture.Helpers.ServicesInstallers.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Initial_Clean_Architecture.API.ServicesInstallers
{
    public class SwaggerServiceInstaller : IServiceInstaller
    {
        private readonly SwaggerSettings _swaggerSettings;

        public SwaggerServiceInstaller()
        {
            _swaggerSettings = new SwaggerSettings();
        }
        public void InstallService(IServiceCollection services, IConfiguration configuration)
        {
            configuration.GetSection(_swaggerSettings.GetType().Name).Bind(_swaggerSettings);

            services.AddSwaggerGen(c =>
            {
                //swagger doc name is related to UIEndpoint
                c.SwaggerDoc(_swaggerSettings.Version, new OpenApiInfo { Title = _swaggerSettings.Title, Version = _swaggerSettings.Version });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Description =
      "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
            });
        }
    }
}
