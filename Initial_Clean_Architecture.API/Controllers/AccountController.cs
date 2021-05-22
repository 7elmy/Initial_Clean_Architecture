using Initial_Clean_Architecture.API.Constants.ApiUrlsConst;
using Initial_Clean_Architecture.API.Extensions;
using Initial_Clean_Architecture.Application.Domain.ContractsModels.Requests;
using Initial_Clean_Architecture.Application.Domain.ContractsModels.Responses;
using Initial_Clean_Architecture.Application.Domain.Interfaces;
using Initial_Clean_Architecture.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Initial_Clean_Architecture.API.Controllers
{
    [Route(BaseUrlConst.BaseUrl)]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly ILoggerService _loggerService;

        public AccountController(IAccountService accountService, ILoggerService loggerService)
        {
            _accountService = accountService;
            _loggerService = loggerService;
        }

        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(LoginResponse))]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(ErrorResponse))]
        [HttpPost(AccountUrlsConst.Register)]
        public async Task<IActionResult> RegisterAsync([FromBody] UserRegistrationRequest request)
        {
            var response = await _accountService.RegisterAsync(request);
            return response.GenerateResponse(_loggerService, HttpContext);
        }


        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(LoginResponse))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ErrorResponse))]
        [HttpPost(AccountUrlsConst.Login)]
        public async Task<IActionResult> LoginAsync([FromBody] UserLoginRequest request)
        {
            var response = await _accountService.LoginAsync(request.Email, request.Password);
            return response.GenerateResponse(_loggerService, HttpContext);
        }

    }
}
