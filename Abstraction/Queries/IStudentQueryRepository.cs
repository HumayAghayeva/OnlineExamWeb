using Domain.DTOs.Read;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstraction.Queries
{
    public interface IStudentQueryRepository
    {

        Task<StudentReadDTO> GetStudentById(int id,
                            CancellationToken cancellationToken);

        Task<StudentReadDTO> GetStudents(CancellationToken cancellationToken);  
    }
}
