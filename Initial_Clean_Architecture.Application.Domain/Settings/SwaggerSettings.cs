using Initial_Clean_Architecture.Application.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Initial_Clean_Architecture.Application.Domain.Settings
{
    public class SwaggerSettings 
    {
        public string Title { get; set; }
        public string Version { get; set; }
        public string UIEndpoint { get { return "/swagger/" + Version + "/swagger.json"; } }
    }
}
