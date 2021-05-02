using Initial_Clean_Architecture.API.Filters;
using Initial_Clean_Architecture.Application.Domain.ContractsModels.Responses;
using Initial_Clean_Architecture.Helpers.ServicesInstallers.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
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

        private void HandelInvalidModel(ApiBehaviorOptions options)
        {
            options.InvalidModelStateResponseFactory = context =>
            {
                var errorResponse = new ErrorResponse();
                var errors = context.ModelState.ToList();

                foreach (var keyModelStatePair in context.ModelState)
                {
                    var key = keyModelStatePair.Key;
                    var errorss = keyModelStatePair.Value.Errors;

                    AddErrorResponse(errorResponse, key, errorss);
                }
                return new UnprocessableEntityObjectResult(errorResponse);
            };
        }

        private static void AddErrorResponse(ErrorResponse errorResponse, string key, ModelErrorCollection errorss)
        {
            if (errorss != null && errorss.Count > 0)
            {
                foreach (var error in errorss)
                {
                    var errorMessage = new ErrorResponse.ErrorMessage()
                    {
                        Key = key,
                        Message = error.ErrorMessage
                    };
                    errorResponse.ErrorMessages.Add(errorMessage);
                }
            }
        }
    }
}
