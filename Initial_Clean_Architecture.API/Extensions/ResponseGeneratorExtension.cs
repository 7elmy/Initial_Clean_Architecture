using Initial_Clean_Architecture.Application.Domain.ContractsModels.Responses;
using Initial_Clean_Architecture.Application.Domain.Interfaces;
using Initial_Clean_Architecture.Data.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Initial_Clean_Architecture.API.Extensions
{
    public static class ResponseGeneratorExtension
    {
        public static IActionResult GenerateResponse(this ResponseState response, ILoggerService loggerService, HttpContext context)
        {
            object resObj = response;
            if (!response.IsValid)
            {
                resObj = response.ErrorResponse;

                loggerService.LogAsync(context, new Log()
                {
                    LogLevel = LogLevel.Error,
                    Message = response.ErrorResponse?.ErrorMessages[0]?.Message,
                });
            }
            var result = new ObjectResult(resObj);
            result.StatusCode = response.ResponseCode;

            return result;
        }
    }
}
