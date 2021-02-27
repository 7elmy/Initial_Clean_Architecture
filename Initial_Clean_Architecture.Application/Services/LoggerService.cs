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

namespace Initial_Clean_Architecture.Application.Services
{
    public class LoggerService : ILoggerService
    {
        private readonly ILogger<LoggerService> _logger;
        private readonly IOptions<LoggingSettings> _loggingSettings;
        private readonly ILoggerDataService _loggerDataService;

        public LoggerService(IOptions<LoggingSettings> loggingSettings, ILoggerDataService loggerDataService)
        {
            var factory = LoggerFactory.Create(cfg => cfg.AddConsole());

            _logger = factory.CreateLogger<LoggerService>();
            _loggingSettings = loggingSettings;
            _loggerDataService = loggerDataService;
        }
        public async Task LogAsync(LogLevel logLevel, string message, Exception exception = null, params object[] args)
        {
            var logValue = _loggingSettings.Value;
            LogLevel logDevelopmentValue;
            LogLevel logProductionValue;

            var isLogDevelopmentValueValid = Enum.TryParse(logValue.Development.LogDetails.LogLevel, out logDevelopmentValue);
            var isLogProductionValueValid = Enum.TryParse(logValue.Production.LogDetails.LogLevel, out logProductionValue);

            if (isLogDevelopmentValueValid && (int)logLevel >= (int)logDevelopmentValue)
            {
                _logger.Log(logLevel, exception, message, args);
            }
            if (isLogProductionValueValid && (int)logLevel >= (int)logProductionValue)
            {
                await _loggerDataService.LogAsync(LogLevel.Debug, exception, message, args);
            }
        }


    }
}
