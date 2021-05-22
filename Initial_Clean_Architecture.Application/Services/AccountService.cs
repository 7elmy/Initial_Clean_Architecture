using AutoMapper;
using Initial_Clean_Architecture.Application.Domain.Constants;
using Initial_Clean_Architecture.Application.Domain.Constants.ErrorsConst;
using Initial_Clean_Architecture.Application.Domain.ContractsModels.Requests;
using Initial_Clean_Architecture.Application.Domain.ContractsModels.Responses;
using Initial_Clean_Architecture.Application.Domain.Interfaces;
using Initial_Clean_Architecture.Application.Domain.Settings;
using Initial_Clean_Architecture.Data.Domain.Entities;
using Initial_Clean_Architecture.Helpers.Exceptions;
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
                }
            }
            return loginResponse.IsValid;
        }

        public async Task<RegistrationResponse> RegisterAsync(UserRegistrationRequest model)
        {
            var registrationResponse = new RegistrationResponse();

            var isValid = await ValidateRegistration(model, registrationResponse);

            if (!isValid)
                return registrationResponse;

            await CreateUser(model, registrationResponse);

            registrationResponse.Email = model.Email;

            return registrationResponse;
        }

        private async Task CreateUser<TResponseModel>(UserRegistrationRequest model, TResponseModel responseModel) where TResponseModel : ResponseState
        {
            var newUser = _mapper.Map<AppUser>(model);

            var creatdUser = await _userManager.CreateAsync(newUser, model.Password);

            if (!creatdUser.Succeeded)
            {
                var errorMessage = new ErrorResponse.ErrorMessage()
                {
                    Key = AccountErrorConst.UserKey,
                    Message = AccountErrorConst.UserFaildToCreate
                };
                responseModel.ErrorResponse.ErrorMessages.Add(errorMessage);
                responseModel.ErrorResponseCode = StatusCodes.Status422UnprocessableEntity;
            }
        }

        private async Task<bool> ValidateRegistration<TResponseModel>(UserRegistrationRequest model, TResponseModel responseModel) where TResponseModel : ResponseState
        {
            ValidateRole(model.Role, responseModel);

            await ValidatePassword(model.Password, model.ConfirmPassword, responseModel);

            await ValidateExistingUser(model.Email,responseModel);

            return responseModel.IsValid;
        }

        private void ValidateRole<TResponseModel>(string role, TResponseModel responseModel) where TResponseModel : ResponseState
        {
            if (!responseModel.IsValid)
                return;

            if (!RolesConst.AllRoles.Contains(role.Trim().ToLower()))
            {
                var errorMessage = new ErrorResponse.ErrorMessage()
                {
                    Key = AccountErrorConst.RoleKey,
                    Message = AccountErrorConst.RoleIsNotFound
                };
                responseModel.ErrorResponse.ErrorMessages.Add(errorMessage);
                responseModel.ErrorResponseCode = StatusCodes.Status422UnprocessableEntity;
            }
        }

        private async Task ValidatePassword<TResponseModel>(string password, string confirmPassword, TResponseModel responseModel) where TResponseModel : ResponseState
        {
            if (!responseModel.IsValid)
                return;

            ValidatePasswordMatching(password, confirmPassword, responseModel);

            await ValidatePasswordSyntax(password, responseModel);
        }

        private async Task ValidatePasswordSyntax<TResponseModel>(string password, TResponseModel responseModel) where TResponseModel : ResponseState
        {
            if (!responseModel.IsValid)
                return;

            var passwordValidator = new PasswordValidator<AppUser>();
            var result = await passwordValidator.ValidateAsync(_userManager, null, password);

            if (!result.Succeeded)
            {
                var errorMessage = new ErrorResponse.ErrorMessage()
                {
                    Key = AccountErrorConst.PasswordKey,
                    Message = AccountErrorConst.PasswordNotValid
                };
                responseModel.ErrorResponse.ErrorMessages.Add(errorMessage);
                responseModel.ErrorResponseCode = StatusCodes.Status422UnprocessableEntity;
            }
        }

        private static void ValidatePasswordMatching<TResponseModel>(string password, string confirmPassword, TResponseModel responseModel) where TResponseModel : ResponseState
        {

            if (!password.Trim().Equals(confirmPassword.Trim()))
            {
                var errorMessage = new ErrorResponse.ErrorMessage()
                {
                    Key = AccountErrorConst.PasswordKey,
                    Message = AccountErrorConst.PasswordMatching
                };
                responseModel.ErrorResponse.ErrorMessages.Add(errorMessage);
                responseModel.ErrorResponseCode = StatusCodes.Status422UnprocessableEntity;
            }
        }

        private async Task ValidateExistingUser<TResponseModel>(string email, TResponseModel responseModel) where TResponseModel : ResponseState
        {
            if (!responseModel.IsValid)
                return;

            var existingUser = await _userManager.FindByEmailAsync(email);

            if (existingUser is not null)
            {
                var errorMessage = new ErrorResponse.ErrorMessage()
                {
                    Key = AccountErrorConst.UserKey,
                    Message = AccountErrorConst.UserAlreadyExist
                };
                responseModel.ErrorResponse.ErrorMessages.Add(errorMessage);
                responseModel.ErrorResponseCode = StatusCodes.Status422UnprocessableEntity;
            }
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
