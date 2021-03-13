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
            var logRepo = _unitOfWork.GetRepository<Log>();

            logRepo.AddAsync(log);

            return _unitOfWork.SaveChangesAsync();
        }


    }
}
