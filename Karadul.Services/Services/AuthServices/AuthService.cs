using Karadul.Data.Entities;
using Karadul.Data.Repository.AuthRepositories;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Karadul.Services.Services.AuthServices
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepository;
        private readonly IConfiguration _configuration;


        public AuthService(IAuthRepository authRepository, IConfiguration configuration)
        {
            _authRepository = authRepository;
            _configuration = configuration;
        }

        public async Task<Admin> AdminLogin(Admin admin)
        {
            var adminLogin = await _authRepository.AdminLogin(admin);
            if (adminLogin == null)
            {
                return null;
            }
            var token = await GenerateTokenAsync(adminLogin.Email, adminLogin.Role.RoleName);
            adminLogin.AccessToken = token;
            await _authRepository.SaveAsync();
            return adminLogin;
        }

        public async Task<string> GenerateTokenAsync(string email, string role)
        {
            var claims = new[]
            {
               new Claim(ClaimTypes.Email, email),
               new Claim(ClaimTypes.Role,role),
            };

            var signinKey = _configuration["Jwt:signinKey"];
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signinKey));
            var credential = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _configuration["Jwt:issuer"],
                audience: _configuration["Jwt:audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(24),
                notBefore: DateTime.Now,
                signingCredentials: credential
            );

            var token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

            return token;
        }
    }
}
