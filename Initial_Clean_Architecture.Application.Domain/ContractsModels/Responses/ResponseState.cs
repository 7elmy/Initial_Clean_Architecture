using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Initial_Clean_Architecture.Application.Domain.ContractsModels.Responses
{
    public abstract class ResponseState
    {
        public ResponseState()
        {
            ErrorResponse = new ErrorResponse();
        }
        public ErrorResponse ErrorResponse { get; set; }
        public bool IsValid { get { return !ErrorResponse.ErrorMessages.Any(); } }
        public int ResponseCode { get; set; } = StatusCodes.Status200OK;
    }
}
