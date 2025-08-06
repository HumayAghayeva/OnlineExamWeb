using Abstraction.Interfaces;
using Domain.Contract;
using Domain.Dtos.Read;
using Domain.DTOs.Write;
using Domain.Entity;
using Domain.Enums;
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
        private readonly IOptions<JWTSettings> _jwtSettings;
        private readonly OEPWriteDB _oEPWriteDB;

        public JwtTokenService(IOptions<JWTSettings> jwtOptions, OEPWriteDB oEPWriteDB)
        {
            _jwtSettings = jwtOptions;
            _oEPWriteDB = oEPWriteDB;
        }

        public async Task<JWTResponseDto> GenerateJwtTokenAsync(string username)
        {
            var student = _oEPWriteDB.Students.FirstOrDefault(s => s.Email == username);

            if (student == null)
            {
                throw new UnauthorizedAccessException("Student not found.");
            }

            var roleIds = _oEPWriteDB.StudentRoles
                .Where(sr => sr.StudentId == student.ID)
                .Select(sr => sr.RoleId)
                .ToList();

            var roleNames = roleIds
                .Select(id => Enum.GetName(typeof(Roles), id))
                .Where(name => !string.IsNullOrEmpty(name))
                .ToList();

            var claims = new List<Claim>
    {
        new Claim(JwtRegisteredClaimNames.Sub, username),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        new Claim(ClaimTypes.Name, username),
        new Claim("studentId", student.ID.ToString())
    };

            foreach (var roleName in roleNames)
            {
                claims.Add(new Claim(ClaimTypes.Role, roleName));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Value.SecretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Value.Issuer,
                audience: _jwtSettings.Value.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.Value.TokenValidityInMinutes),
                signingCredentials: creds
            );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return await Task.FromResult(new JWTResponseDto
            {
                UserName = username,
                Token = jwt,
                Roles = roleNames
            });
        }

    }
}