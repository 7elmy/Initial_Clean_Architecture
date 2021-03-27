using AutoMapper;
using Initial_Clean_Architecture.Application.Domain.ContractsModels.Requests;
using Initial_Clean_Architecture.Application.Domain.ContractsModels.Responses;
using Initial_Clean_Architecture.Application.Domain.Interfaces;
using Initial_Clean_Architecture.Application.Domain.Settings;
using Initial_Clean_Architecture.Data.Domain.Entities;
using Initial_Clean_Architecture.Data.Domain.Interfaces;
using Initial_Clean_Architecture.Helpers.Exceptions;
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
            var loggeninUser = await _userManager.FindByEmailAsync(email);

            //todo handel exception
            if (loggeninUser is null)
                throw new Exception();


            var userHasValidPassword = await _userManager.CheckPasswordAsync(loggeninUser, password);
            //todo handel exception
            if (!userHasValidPassword)
            {
                throw new Exception();
            }

            var claims = GenerateClaimsList(loggeninUser);

            var token = GenerateToken(claims);

            var loginResponse = new LoginResponse()
            {
                Token = token
            };

            return loginResponse;

        }

        public async Task<RegistrationResponse> RegisterAsync(UserRegistrationRequest model)
        {
            var existingUser = await _userManager.FindByEmailAsync(model.Email);

            if (existingUser is not null)
                throw new AlreadyExistException.Email();

            var newUser = _mapper.Map<AppUser>(model);

            //todo: check password
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
