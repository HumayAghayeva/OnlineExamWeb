using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstraction.Write
{
    public interface IStudentPostRepository : IGenericRepository<Student>
    {
        Task PostStudentAsync(Student student,
                              CancellationToken cancellationToken);
    }
}
