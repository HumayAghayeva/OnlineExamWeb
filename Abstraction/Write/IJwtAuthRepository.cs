using Domain.DTOs.JWT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstraction.Write
{
    public interface IJwtAuthRepository
    {
        Task<(int, string)> JwtRegistrationAsync(JwtRegistrationRequestDTO registration,
                                               string role,
                                               CancellationToken cancellationToken);
        Task<(int, string)> JwtLoginAsync(JwtLoginDTO Login,
                                          CancellationToken cancellationToken);
    }
}
