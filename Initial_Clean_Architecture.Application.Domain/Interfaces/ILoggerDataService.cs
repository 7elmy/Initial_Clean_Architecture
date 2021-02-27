using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Initial_Clean_Architecture.Application.Domain.Interfaces
{
    public interface ILoggerDataService
    {
        Task LogAsync(LogLevel logLevel, Exception exception, string message, params object[] args);
    }
}
