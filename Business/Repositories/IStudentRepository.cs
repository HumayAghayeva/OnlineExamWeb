using Abstraction;
using Domain.DTOs.Write;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repositories
{
    public interface IStudentRepository : IRepository<StudentRequestDTO>
    {
    }
}
