using Initial_Clean_Architecture.Application.Domain.ContractsModels.Responses;
using Initial_Clean_Architecture.Application.Domain.Interfaces;
using Initial_Clean_Architecture.Data.Domain.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
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
            //handel not handeled exceptions from GlobalExceptionFilter
            try
            {
                await loggerService.LogAsync(new Log()
                {
                    Class = "test start"
                });

                await _next.Invoke(context);

                await loggerService.LogAsync(new Log()
                {
                    Class = "test end"
                });
            }
            catch (UnauthorizedAccessException e)
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsJsonAsync(new ErrorResponse { Message = e.Message });
            }
            catch (Exception e)
            {
                context.Response.StatusCode = 500;
                await context.Response.WriteAsJsonAsync(new ErrorResponse { Message = e.Message });
            }
            finally
            {
                await loggerService.LogAsync(new Log()
                {
                    Class = "test end"
                });
            }
        }


    }
}
