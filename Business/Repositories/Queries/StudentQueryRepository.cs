using Domain.DTOs.Read;
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
using Domain.DTOs.Write;
using System.Text.RegularExpressions;
using Domain.Enums;
using Domain.Entity.Write;

namespace Business.Repositories
{
    public class StudentQueryRepository : Repository<StudentRequestDTO>, IStudentQueryRepository
    {
        private readonly OEPWriteDB _context; /// <summary>
        /// </summary>
        private readonly DbSet<Student> _entities;

        public StudentQueryRepository(OEPWriteDB context) : base(context)
        {
            _context = context;
            _entities = context.Set<Student>();
        }

        public async Task<StudentResponseDTO> GetStudentById(int id, CancellationToken cancellationToken)
        {
            var student = await _entities.FirstOrDefaultAsync(s => s.ID == id, cancellationToken);

            if (student == null)
                return null;

            return new StudentResponseDTO
            {
                Id = student.ID,
                Name = student.Name,
                LastName = student.LastName,
                Email = student.Email,
                PIN= student.PIN
            };
        }

        public async Task<List<StudentResponseDTO>> GetStudents(CancellationToken cancellationToken)
        {
            var students= await _entities.ToListAsync(cancellationToken);

            var studentDtos = students.Select(s => new StudentResponseDTO
            {
                Id = s.ID,
                Name = s.Name,
                LastName= s.LastName,
                PIN = s.PIN,
                Email=s.Email,
                GroupName=  Enum.GetName(typeof(Groups), s.GroupId)
            }).ToList();

            return studentDtos;
        }
    }
}
