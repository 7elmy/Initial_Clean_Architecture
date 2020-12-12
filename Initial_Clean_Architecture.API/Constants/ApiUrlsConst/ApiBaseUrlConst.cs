using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Initial_Clean_Architecture.API.Constants.ApiUrlsConst
{
    public class ApiBaseUrlConst
    {
        private const string _version = "v1";
        internal readonly string _baseUrl = "api/" + _version + "/";
    }
}
