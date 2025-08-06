using Domain.Dtos.Read;
using Domain.DTOs.Write;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstraction.Interfaces
{
    public interface IJwtTokenService
    {
        Task<JWTResponseDto> GenerateJwtTokenAsync(string username);
    }
}
