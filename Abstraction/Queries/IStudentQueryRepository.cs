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

        Task<StudentResponseDTO> GetStudentById(int id,
                            CancellationToken cancellationToken);

        Task<List<StudentResponseDTO>> GetStudents(CancellationToken cancellationToken);  
    }
}
