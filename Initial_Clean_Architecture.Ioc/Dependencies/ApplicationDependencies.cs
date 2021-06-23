using Initial_Clean_Architecture.Application.Domain.Interfaces;
using Initial_Clean_Architecture.Application.Domain.Interfaces.AccountService;
using Initial_Clean_Architecture.Application.Services;
using Initial_Clean_Architecture.Application.Services.AccountService;
using Initial_Clean_Architecture.Ioc.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Initial_Clean_Architecture.Ioc.Dependencies
{
    public class ApplicationDependencies : IRegister
    {
        public void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<ILoggerService, LoggerService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IAccountServiceValidator, AccountServiceValidator>();
        }
    }
}
