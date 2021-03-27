using Initial_Clean_Architecture.Application.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using Initial_Clean_Architecture.Application.Domain.Settings;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging.Console;
using Initial_Clean_Architecture.Data.Domain.Interfaces;
using Initial_Clean_Architecture.Data.Domain.Entities;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.AspNetCore.Http;
using System.Diagnostics;
using Microsoft.AspNetCore.WebUtilities;
using System.IO;

namespace Initial_Clean_Architecture.Application.Services
{
    public class LoggerService : ILoggerService
    {
        private IUnitOfWork _unitOfWork;

        public LoggerService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public Task<int> LogAsync(Log log)
        {
            return AddLog(log);
        }

        public Task<int> LogAsync(HttpContext context, Log log, bool isResponse)
        {
            log.Path = context.Request.Path;
            log.Method = context.Request.Method;
            log.TraceIdentifier = context.TraceIdentifier;
            if (isResponse)
            {
                log.ResponseStatusCode = (int)context.Response.StatusCode;
                log.ResponseStatusMessage = ReasonPhrases.GetReasonPhrase(log.ResponseStatusCode);
            }
            return AddLog(log);
        }

        private Task<int> AddLog(Log log)
        {
            var logRepo = _unitOfWork.GetRepository<Log>();
            logRepo.AddAsync(log);

            return _unitOfWork.SaveChangesAsync();
        }
    }
}
