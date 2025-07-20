using Abstraction.Interfaces;
using Domain.DTOs.Write;
using Domain.OptionDP;
using Infrastructure.DataContext.Write;
using Infrastructure.Persistent.Read;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver.Linq;
using OnlineExamWeb.Utilities;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services
{

    public class JwtTokenService : IJwtTokenService
    {
        private readonly JWTSettings _jwtSetting;
        private readonly OEPWriteDB _oEPWriteDB;

        public JwtTokenService(IOptions<JWTSettings> jwtOptions, OEPWriteDB oEPWriteDB)
        {
            _jwtSetting = jwtOptions.Value;
            _oEPWriteDB = oEPWriteDB;
        }

        public async Task<string> GenerateJwtTokenAsync(string username)
        {
            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, username),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };
           
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSetting.SecretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _jwtSetting.Issuer,
                audience: _jwtSetting.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtSetting.TokenValidityInMinutes),
                signingCredentials: creds
            );
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return await Task.FromResult(jwt);
        }

        public async Task<string> AuthenticateAsync(string email , string password)
        {
            if (
                string.IsNullOrWhiteSpace(email) ||
                string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentException("Email or password is missing.");
            }

            var userAccount =  _oEPWriteDB.Students.FirstOrDefault(w => w.Email == email && w.Password == password);

            if (userAccount == null)
            {
                throw new UnauthorizedAccessException("Invalid credentials.");
            }

             return await GenerateJwtTokenAsync(userAccount.Email);
        }
    }
}