using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Initial_Clean_Architecture.Application.Domain.ContractsModels.Responses
{
    public class ErrorResponse
    {
        public ErrorResponse()
        {
            ErrorMessages = new List<ErrorMessage>();
        }
        public List<ErrorMessage> ErrorMessages { get; set; }
        public string Exeption { get; set; }

        public class ErrorMessage
        {
            public string Key { get; set; }
            public string Message { get; set; }
        }
    }
}
