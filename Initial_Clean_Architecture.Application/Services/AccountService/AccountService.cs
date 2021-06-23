using AutoMapper;
using Initial_Clean_Architecture.Application.Domain.Constants;
using Initial_Clean_Architecture.Application.Domain.Constants.ErrorsConst;
using Initial_Clean_Architecture.Application.Domain.ContractsModels.Requests;
using Initial_Clean_Architecture.Application.Domain.ContractsModels.Responses;
using Initial_Clean_Architecture.Application.Domain.Interfaces.AccountService;
using Initial_Clean_Architecture.Application.Domain.Settings;
using Initial_Clean_Architecture.Data.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Initial_Clean_Architecture.Application.Services.AccountService
{
    public class AccountService : IAccountService
    {
        private readonly IAccountServiceValidator _accountServiceValidator;
        private readonly UserManager<AppUser> _userManager;
        private readonly JWTSettings _jwtSettings;
        private readonly IMapper _mapper;

        public AccountService(IAccountServiceValidator accountServiceValidator, UserManager<AppUser> userManager, IOptions<JWTSettings> jwtSettings, IMapper mapper)
        {
            _accountServiceValidator = accountServiceValidator;
            _userManager = userManager;
            _mapper = mapper;
            _jwtSettings = jwtSettings.Value;
        }

        public async Task<LoginResponse> LoginAsync(string email, string password)
        {

            var loggedinUser = await _userManager.FindByEmailAsync(email);

            var loginValidator = await _accountServiceValidator.LoginValidator(password, loggedinUser);

            if (!loginValidator.IsValid)
            {
                var loginValidatorResponse = new LoginResponse(loginValidator);
                return loginValidatorResponse;
            }

            var claims = GenerateClaimsList(loggedinUser);

            var token = GenerateToken(claims);

            var loginResponse = new LoginResponse();

            loginResponse.Token = token;

            return loginResponse;

        }

        public async Task<RegistrationResponse> RegisterAsync(UserRegistrationRequest model)
        {
            var registerValidator = await _accountServiceValidator.RegisterValidator(model);

            if (!registerValidator.IsValid)
            {
                var registerValidatorResponse = new RegistrationResponse(registerValidator);
                return registerValidatorResponse;
            }

            var newUser = _mapper.Map<AppUser>(model);

            var createUserResponse = await CreateUser(newUser, model.Password);

            if (!createUserResponse.IsValid)
            {
                var registerValidatorResponse = new RegistrationResponse(createUserResponse);
                return registerValidatorResponse;
            }

            var registrationResponse = new RegistrationResponse();

            registrationResponse.Email = model.Email;

            return registrationResponse;
        }

        private async Task<ResponseState> CreateUser(AppUser model, string password)
        {
            var creatdUser = await _userManager.CreateAsync(model, password);

            var response = new ResponseState();

            if (!creatdUser.Succeeded)
            {
                var errorMessage = new ErrorResponse.ErrorMessage()
                {
                    Key = AccountErrorConst.UserKey,
                    Message = AccountErrorConst.UserFaildToCreate
                };
                response.ErrorResponse.ErrorMessages.Add(errorMessage);
                response.ResponseCode = StatusCodes.Status422UnprocessableEntity;
            }
            return response;
        }

        private List<Claim> GenerateClaimsList(AppUser user)
        {
            var claims = new List<Claim> {
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(ClaimTypes.Role, user.Role)
                };

            return claims;
        }

        private string GenerateToken(List<Claim> claims)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.Add(_jwtSettings.TokenLifeTime),
                Issuer = _jwtSettings.Issuer,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            var formatedToken = tokenHandler.WriteToken(token);

            return formatedToken;
        }
    }
}
