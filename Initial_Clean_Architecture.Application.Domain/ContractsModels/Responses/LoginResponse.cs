using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Initial_Clean_Architecture.Application.Domain.ContractsModels.Responses
{
    public class LoginResponse : ResponseState
    {
        public LoginResponse()
        {

        }
        public LoginResponse(ResponseState responseState) : base(responseState)
        {

        }
        public string Token { get; set; }
    }
}
