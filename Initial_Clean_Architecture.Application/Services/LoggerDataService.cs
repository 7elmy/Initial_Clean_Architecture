using Initial_Clean_Architecture.Application.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Initial_Clean_Architecture.Application.Services
{
    /// <summary>
    /// use this to save your Log Data
    /// </summary>
    public class LoggerDataService : ILoggerDataService
    {
        public async Task LogAsync(LogLevel logLevel, Exception exception, string message, params object[] args)
        {
            throw new NotImplementedException();
        }
    }
}
