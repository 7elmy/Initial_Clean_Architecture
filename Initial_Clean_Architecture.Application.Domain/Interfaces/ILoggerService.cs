using Initial_Clean_Architecture.Data.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Initial_Clean_Architecture.Application.Domain.Interfaces
{
    public interface ILoggerService
    {
        Task<int> LogAsync(Log log);
        Task<int> LogAsync(HttpContext context, Log log, bool isResponse=false);
    }
}
