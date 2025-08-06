using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dtos.Read
{
    public record JWTResponseDto
    {
        public string UserName { get; init; }
        public string Token { get; init; }
        public IEnumerable<string> Roles { get; init; }
    }
}
