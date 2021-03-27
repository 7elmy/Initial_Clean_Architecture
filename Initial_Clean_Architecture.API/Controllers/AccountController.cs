using Initial_Clean_Architecture.API.Constants.ApiUrlsConst;
using Initial_Clean_Architecture.Application.Domain.ContractsModels.Requests;
using Initial_Clean_Architecture.Application.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Initial_Clean_Architecture.API.Controllers
{
    [Route(BaseUrlConst.BaseUrl)]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [AllowAnonymous]
        [HttpPost(AccountUrlsConst.Register)]
        public async Task<IActionResult> RegisterAsync([FromBody] UserRegistrationRequest request)
        {
            var authResponse = await _accountService.RegisterAsync(request);
            return Ok(authResponse);
        }

        [AllowAnonymous]
        [HttpPost(AccountUrlsConst.Login)]
        public async Task<IActionResult> LoginAsync([FromBody] UserLoginRequest request)
        {
            var loginResponse = await _accountService.LoginAsync(request.Email, request.Password);
            return Ok(loginResponse);
        }
    }
}
