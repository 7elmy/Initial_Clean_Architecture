using AutoMapper;
using Initial_Clean_Architecture.Application.Domain.Constants.ErrorsConst;
using Initial_Clean_Architecture.Application.Domain.ContractsModels.Requests;
using Initial_Clean_Architecture.Application.Domain.ContractsModels.Responses;
using Initial_Clean_Architecture.Application.Domain.Interfaces;
using Initial_Clean_Architecture.Application.Domain.Settings;
using Initial_Clean_Architecture.Data.Domain.Entities;
using Initial_Clean_Architecture.Data.Domain.Interfaces;
using Initial_Clean_Architecture.Helpers.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Initial_Clean_Architecture.Application.Services
{
    public class AccountService : IAccountService
    {

        private readonly UserManager<AppUser> _userManager;
        private readonly JWTSettings _jwtSettings;
        private readonly IMapper _mapper;

        public AccountService(UserManager<AppUser> userManager, IOptions<JWTSettings> jwtSettings, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
            _jwtSettings = jwtSettings.Value;
        }

        public async Task<LoginResponse> LoginAsync(string email, string password)
        {
            var loginResponse = new LoginResponse();

            var loggedinUser = await _userManager.FindByEmailAsync(email);

            var isValid = await ValidateLogin(password, loginResponse, loggedinUser);

            if (!isValid)
                return loginResponse;

            var claims = GenerateClaimsList(loggedinUser);

            var token = GenerateToken(claims);

            loginResponse.Token = token;

            return loginResponse;

        }

        private async Task<bool> ValidateLogin(string password, LoginResponse loginResponse, AppUser loggedinUser)
        {
            var isValid = true;
            if (loggedinUser is null)
            {
                var userHasValidPassword = await _userManager.CheckPasswordAsync(loggedinUser, password);

                if (!userHasValidPassword)
                {
                    var errorMessage = new ErrorResponse.ErrorMessage()
                    {
                        Key = AccountErrorConst.UserKey,
                        Message = AccountErrorConst.EmailOrPassNotCorrect
                    };
                    loginResponse.ErrorResponse.ErrorMessages.Add(errorMessage);
                    loginResponse.ErrorResponseCode = StatusCodes.Status401Unauthorized;
                    isValid = false;
                }
            }
            return isValid;
        }

        public async Task<RegistrationResponse> RegisterAsync(UserRegistrationRequest model)
        {
            await ValidateRegistration(model);

            var newUser = _mapper.Map<AppUser>(model);

            //todo: check role

            var creatdUser = await _userManager.CreateAsync(newUser, model.Password);
            //todo handel exception
            if (!creatdUser.Succeeded)
                throw new Exception();

            var registrationResponse = new RegistrationResponse()
            {
                Email = newUser.Email
            };
            return registrationResponse;
        }

        private async Task ValidateRegistration(UserRegistrationRequest model)
        {
            await ValidatePassword(model);

            await ValidateExistingUser(model);
        }

        private async Task ValidatePassword(UserRegistrationRequest model)
        {
            var passwordValidator = new PasswordValidator<AppUser>();
            var result = await passwordValidator.ValidateAsync(_userManager, null, model.Password);

            if (!result.Succeeded)
                throw new Exception("passsowrd incorrect");
        }

        private async Task ValidateExistingUser(UserRegistrationRequest model)
        {
            var existingUser = await _userManager.FindByEmailAsync(model.Email);

            if (existingUser is not null)
                throw new AlreadyExistException.Email();
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
