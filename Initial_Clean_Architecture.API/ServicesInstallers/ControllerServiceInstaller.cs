using Initial_Clean_Architecture.API.Filters;
using Initial_Clean_Architecture.Application.Domain.ContractsModels.Responses;
using Initial_Clean_Architecture.Helpers.ServicesInstallers.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Initial_Clean_Architecture.API.ServicesInstallers
{
    public class ControllerServiceInstaller : IServiceInstaller
    {
        public void InstallService(IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers(options =>
             options.Filters.Add(typeof(GlobalExceptionFilter))
            ).ConfigureApiBehaviorOptions(options =>
            {
                HandelInvalidModel(options);
            });
        }

        private static void HandelInvalidModel(ApiBehaviorOptions options)
        {
            options.InvalidModelStateResponseFactory = context =>
            {
                var errorMessag = "";
                var error = context.ModelState.ToList().FirstOrDefault();

                errorMessag = (string.IsNullOrWhiteSpace(error.Key) ? "" : error.Key.ToString() + ": ") + error.Value.Errors.FirstOrDefault().ErrorMessage.Split(". Path:").FirstOrDefault();

                return new BadRequestObjectResult(new ErrorResponse { Message = errorMessag });
            };
        }
    }
}
