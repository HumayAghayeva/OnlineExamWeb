using Domain.DTOs.Read;
using Domain.DTOs.Write;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstraction.Write
{
    public interface IStudentPostRepository
    {
        Task PostStudentAsync(StudentRequestDTO model,
                            CancellationToken cancellationToken);
    }
}
