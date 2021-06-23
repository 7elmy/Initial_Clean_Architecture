using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Initial_Clean_Architecture.Application.Domain.ContractsModels.Responses
{
    public class RegistrationResponse: ResponseState
    {
        public RegistrationResponse()
        {

        }
        public RegistrationResponse(ResponseState responseState) : base(responseState)
        {

        }
        public string Email { get; set; }
    }
}
