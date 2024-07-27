using Domain.DTOs.Read;
using Domain.DTOs.Write;
using System;
using System.Collections.Generic;
using FluentValidation.Results;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstraction.Write
{
    public interface IStudentPostRepository
    {
        Task<ValidationResult> PostStudentAsync(StudentRequestDTO model,
                            CancellationToken cancellationToken);

        Task<StudentReadDTO> GetStudentById(int id,
                            CancellationToken cancellationToken);

        Task UpdateStudentAsync(int id, StudentRequestDTO model,
                            CancellationToken cancellationToken);
    }
}
