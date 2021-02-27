using System;
using System.Collections.Generic;
using System.Text;

namespace Initial_Clean_Architecture.Application.Domain.Settings
{
    public class LoggingSettings
    {
        public Development Development { get; set; }
        public Production Production { get; set; }
    }

    public class Development
    {
        public LogDetails LogDetails { get; set; }
    }

    public class Production
    {
        public LogDetails LogDetails { get; set; }
    }

    public class LogDetails
    {
        public string LogLevel { get; set; }
        public bool LogToDb { get; set; }
    }






}
