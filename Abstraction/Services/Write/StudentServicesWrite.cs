using Abstraction.Write;
using Domain.DTOs.Write;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstraction.Services.Write
{
    public class StudentServicesWrite: IStudentPostRepository
    {
        private readonly IUnitOfWork uow;
        public StudentServicesWrite(IUnitOfWork _uow)
        {
            uow = _uow;
        }
        public Task PostStudentAsync(StudentRequestDTO studentRequestDTO,CancellationToken token)
        {

            return Task.FromResult(0);  
        }

    }
}
