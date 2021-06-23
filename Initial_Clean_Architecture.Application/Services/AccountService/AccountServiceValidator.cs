using Initial_Clean_Architecture.Application.Domain.Constants;
using Initial_Clean_Architecture.Application.Domain.Constants.ErrorsConst;
using Initial_Clean_Architecture.Application.Domain.ContractsModels.Requests;
using Initial_Clean_Architecture.Application.Domain.ContractsModels.Responses;
using Initial_Clean_Architecture.Application.Domain.Interfaces.AccountService;
using Initial_Clean_Architecture.Data.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Initial_Clean_Architecture.Application.Services.AccountService
{
    class AccountServiceValidator : IAccountServiceValidator
    {
        private readonly UserManager<AppUser> _userManager;
        public AccountServiceValidator(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<ResponseState> LoginValidator(string password, AppUser loggedinUser)
        {
            var response = new ResponseState();

            if (loggedinUser is null || !(await IsValidPassword(password, loggedinUser)))
            {
                var errorMessage = new ErrorResponse.ErrorMessage()
                {
                    Key = AccountErrorConst.UserKey,
                    Message = AccountErrorConst.EmailOrPassNotCorrect
                };
                response.ErrorResponse.ErrorMessages.Add(errorMessage);
                response.ResponseCode = StatusCodes.Status401Unauthorized;

            }
            return response;
        }

        private async Task<bool> IsValidPassword(string password, AppUser loggedinUser)
        {
            return await _userManager.CheckPasswordAsync(loggedinUser, password);
        }

        public async Task<ResponseState> RegisterValidator(UserRegistrationRequest model)
        {
            var response = ValidateRole(model.Role);
            if (!response.IsValid)
                return response;

            response = await ValidatePassword(model.Password, model.ConfirmPassword);
            if (!response.IsValid)
                return response;

            response = await ValidateExistingUser(model.Email);
            if (!response.IsValid)
                return response;

            return response;
        }


        private ResponseState ValidateRole(string role)
        {
            var response = new ResponseState();

            if (!RolesConst.IsExist(role))
            {
                var errorMessage = new ErrorResponse.ErrorMessage()
                {
                    Key = AccountErrorConst.RoleKey,
                    Message = AccountErrorConst.RoleIsNotFound
                };
                response.ErrorResponse.ErrorMessages.Add(errorMessage);
                response.ResponseCode = StatusCodes.Status422UnprocessableEntity;
            }

            return response;
        }

        private async Task<ResponseState> ValidatePassword(string password, string confirmPassword)
        {
            var response = ValidatePasswordMatching(password, confirmPassword);
            if (!response.IsValid)
                return response;

            response = await ValidatePasswordSyntax(password);
            if (!response.IsValid)
                return response;

            return response;
        }

        private async Task<ResponseState> ValidatePasswordSyntax(string password)
        {
            var response = new ResponseState();

            var passwordValidator = new PasswordValidator<AppUser>();
            var result = await passwordValidator.ValidateAsync(_userManager, null, password);

            if (!result.Succeeded)
            {
                var errorMessage = new ErrorResponse.ErrorMessage()
                {
                    Key = AccountErrorConst.PasswordKey,
                    Message = AccountErrorConst.PasswordNotValid
                };
                response.ErrorResponse.ErrorMessages.Add(errorMessage);
                response.ResponseCode = StatusCodes.Status422UnprocessableEntity;
            }

            return response;
        }

        private ResponseState ValidatePasswordMatching(string password, string confirmPassword)
        {
            var response = new ResponseState();

            if (!password.Trim().Equals(confirmPassword.Trim()))
            {
                var errorMessage = new ErrorResponse.ErrorMessage()
                {
                    Key = AccountErrorConst.PasswordKey,
                    Message = AccountErrorConst.PasswordMatching
                };
                response.ErrorResponse.ErrorMessages.Add(errorMessage);
                response.ResponseCode = StatusCodes.Status422UnprocessableEntity;
            }
            return response;
        }

        private async Task<ResponseState> ValidateExistingUser(string email)
        {
            var response = new ResponseState();

            var existingUser = await _userManager.FindByEmailAsync(email);

            if (existingUser is not null)
            {
                var errorMessage = new ErrorResponse.ErrorMessage()
                {
                    Key = AccountErrorConst.UserKey,
                    Message = AccountErrorConst.UserAlreadyExist
                };
                response.ErrorResponse.ErrorMessages.Add(errorMessage);
                response.ResponseCode = StatusCodes.Status422UnprocessableEntity;
            }

            return response;
        }

    }
}
