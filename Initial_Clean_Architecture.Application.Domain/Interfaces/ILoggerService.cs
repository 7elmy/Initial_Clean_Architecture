using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Initial_Clean_Architecture.Application.Domain.Interfaces
{
    public interface ILoggerService
    {
        Task LogAsync(LogLevel logLevel, string message, Exception exception = null, params object[] args);
    }
}
