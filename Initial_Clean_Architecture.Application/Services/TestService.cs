using Initial_Clean_Architecture.Application.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Initial_Clean_Architecture.Application.Services
{
    public class TestService : ITestService
    {
        private readonly ILoggerService _loggerService;
        private readonly ILogger<TestService> _logger;
      

        public TestService(ILoggerService loggerService, ILogger<TestService> logger)
        {
            _loggerService = loggerService;
            _logger = logger;
        }
        public void LogTest()
        {
            
        }
    }
}
