using System;
using System.Collections.Generic;
using System.Text;

namespace Initial_Clean_Architecture.Helpers.Constants.ApiUrlsConst
{
    public class ApiBaseUrlConst
    {
        private const string _version = "v1";
        internal readonly string _baseUrl = "api/" + _version + "/";
    }
}
