using Domain.DTOs.Read;
using Domain.Entity;
using Abstraction.Command;
using Infrastructure.DataContext.Write;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abstraction.Queries;
using System.Diagnostics.Metrics;
using Domain.DTOs.Read;
using Domain.DTOs.Write;

namespace Business.Repositories
{
    public class StudentQueryRepository : Repository<StudentRequestDTO>, IStudentQueryRepository
    {
        private readonly OEPWriteDB _context;
        private readonly DbSet<Student> entities;

        public StudentQueryRepository(OEPWriteDB context) : base(context)
        {
            _context = context;
            entities = context.Set<Student>();
        }

        public Task<StudentReadDTO> GetStudentById(int id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<StudentReadDTO> GetStudents(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
