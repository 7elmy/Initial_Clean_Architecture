using Initial_Clean_Architecture.Data.Domain.Constants;
using Initial_Clean_Architecture.Data.Domain.Entities.Common;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Initial_Clean_Architecture.Data.Domain.Entities
{
    public class Log : ICreationDate
    {
        public long Id { get; set; }
        public LogLevel LogLevel { get; set; }
        public string Message { get; set; }
        public string Exception { get; set; }
        [MaxLength(100)]
        public string Class { get; set; }
        [MaxLength(100)]
        public string Method { get; set; }
        public string UserName { get; set; }
        public string Proprties { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
