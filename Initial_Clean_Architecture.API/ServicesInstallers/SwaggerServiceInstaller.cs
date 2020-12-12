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
        private readonly string _title = "Bostan Online API";
        private readonly string _version = "V1";


        public void InstallService(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = _title, Version = _version });

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
