using Domain.DTOs.Read;
using Domain.DTOs.Write;
using System;
using System.Collections.Generic;
using FluentValidation.Results;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstraction.Command
{
    public interface IStudentCommandRepository : IRepository<StudentRequestDTO>
    {
        
    }
}
