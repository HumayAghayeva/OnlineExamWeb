using Domain.DTOs.Write;
using Domain.Models;
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
using Domain.DTOs.Read;

namespace Business.Repositories.Command
{
    public class StudentCommandRepository : Repository<StudentRequestDTO>,IStudentCommandRepository
    {
        private readonly OEPWriteDB _context;
        private readonly DbSet<Student> entities;

        public StudentCommandRepository(OEPWriteDB context) : base(context)
        {
            _context = context;
            entities = context.Set<Student>();
        }

        public Task AddStudentById(StudentReadDTO studentReadDTO, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
