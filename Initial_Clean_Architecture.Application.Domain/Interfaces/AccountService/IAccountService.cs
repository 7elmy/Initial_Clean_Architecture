using Initial_Clean_Architecture.Application.Domain.ContractsModels.Requests;
using Initial_Clean_Architecture.Application.Domain.ContractsModels.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Initial_Clean_Architecture.Application.Domain.Interfaces.AccountService
{
    public interface IAccountService
    {
        Task<RegistrationResponse> RegisterAsync(UserRegistrationRequest model);
        Task<LoginResponse> LoginAsync(string email, string password);
    }
}
