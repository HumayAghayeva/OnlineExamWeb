using Domain.DTOs.JWT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstraction.Write
{
    public interface IUserPostRepository
    {
        Task PostUserAsync(JwtRegistrationRequestDTO registrationRequestDTO,
            CancellationToken cancellationToken);
    }
}
