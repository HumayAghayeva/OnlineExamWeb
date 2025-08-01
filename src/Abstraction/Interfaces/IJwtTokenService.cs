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
        Task<string> GenerateJwtTokenAsync(string username);
    }
}
