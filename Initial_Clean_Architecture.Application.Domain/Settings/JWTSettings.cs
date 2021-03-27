using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Initial_Clean_Architecture.Application.Domain.Settings
{
    public class JWTSettings
    {
        public string Secret { get; set; }
        public TimeSpan TokenLifeTime { get; set; }
        public string Issuer { get; set; }
    }
}
