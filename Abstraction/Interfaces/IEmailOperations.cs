using Domain.DTOs.Read;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstraction.Interfaces
{
    public interface IEmailOperations
    {
        public Task<EmailResponse> SendEmail(int studentId,CancellationToken cancellationToken = default);
    }
}
