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

namespace Business.Repositories
{
    public class StudentRepository : Repository<StudentRequestDTO>, IStudentRepository
    {
        private readonly DBConn _context;
        private readonly DbSet<Student> entities;
        public StudentRepository(DBConn context) : base(context)
        {
            _context = context;
            entities = context.Set<Student>();
        }

    }
}
