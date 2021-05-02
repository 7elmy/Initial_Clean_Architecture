using Initial_Clean_Architecture.Application.Domain.ContractsModels.Responses;
using Initial_Clean_Architecture.Application.Domain.Interfaces;
using Initial_Clean_Architecture.Data.Domain.Entities;
using Initial_Clean_Architecture.Helpers.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Initial_Clean_Architecture.API.Filters
{
    public class GlobalExceptionFilter : Attribute, IExceptionFilter
    {
        private readonly ILoggerService _loggerService;

        public GlobalExceptionFilter(ILoggerService loggerService)
        {
            _loggerService = loggerService;
        }

        public void OnException(ExceptionContext context)
        {
            context.ExceptionHandled = true;

            var exception = context.Exception.Message;

            if (context.Exception is Exception)
            {
                var errorResponse = new ErrorResponse();
                errorResponse.Exeption = exception;
                context.Result = new BadRequestObjectResult(errorResponse);
            }

            _loggerService.LogAsync(context.HttpContext, new Log()
            {
                Exception = exception,
                Message = nameof(Exception),
                LogLevel = LogLevel.Warning,
            }, isResponse: true);
        }
    }
}
