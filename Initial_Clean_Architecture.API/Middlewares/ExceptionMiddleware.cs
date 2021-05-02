using Initial_Clean_Architecture.Application.Domain.ContractsModels.Responses;
using Initial_Clean_Architecture.Application.Domain.Interfaces;
using Initial_Clean_Architecture.Data.Domain.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Initial_Clean_Architecture.API.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, ILoggerService loggerService)
        {
            await BeginInvoke(context, loggerService);
        }

        private async Task BeginInvoke(HttpContext context, ILoggerService loggerService)
        {
            try
            {
                await loggerService.LogAsync(context, new Log()
                {
                    LogLevel = LogLevel.Information,
                    Message = nameof(context.Request)
                });

                await _next.Invoke(context);

                await loggerService.LogAsync(context, new Log()
                {
                    LogLevel = LogLevel.Information,
                    Message = nameof(context.Response)
                }, isResponse: true);
            }
            catch (UnauthorizedAccessException e)
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                var errorResponse = new ErrorResponse();
                errorResponse.Exeption = e.Message;

                await context.Response.WriteAsJsonAsync(errorResponse);

                await loggerService.LogAsync(context, new Log()
                {
                    LogLevel = LogLevel.Warning,
                    Exception = e.Message,
                    Message = nameof(UnauthorizedAccessException),
                }, isResponse: true);
            }
            catch (Exception e)
            {
                context.Response.StatusCode = 500;
                var errorResponse = new ErrorResponse();
                errorResponse.Exeption = e.Message;

                await context.Response.WriteAsJsonAsync(errorResponse);

                await loggerService.LogAsync(context, new Log()
                {
                    LogLevel = LogLevel.Error,
                    Exception = e.Message,
                    Message = nameof(Exception),
                }, isResponse: true);
            }
        }


    }
}
