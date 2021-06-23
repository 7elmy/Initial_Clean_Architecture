﻿using Initial_Clean_Architecture.Application.Domain.ContractsModels.Requests;
using Initial_Clean_Architecture.Application.Domain.ContractsModels.Responses;
using Initial_Clean_Architecture.Data.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Initial_Clean_Architecture.Application.Domain.Interfaces.AccountService
{
    public interface IAccountServiceValidator
    {
        Task<ResponseState> RegisterValidator(UserRegistrationRequest model);
        Task<ResponseState> LoginValidator(string password, AppUser loggedinUser);
    }
}
