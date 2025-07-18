using Domain.Dtos.Read;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstraction.Interfaces
{
    public interface IEmailOperations
    {
         Task<EmailResponseDto> SendEmail(int studentId,CancellationToken cancellationToken = default);
    }
}
