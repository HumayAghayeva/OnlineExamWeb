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
using Domain.DTOs.Write;

namespace Business.Repositories
{
    public class StudentQueryRepository : Repository<StudentRequestDTO>, IStudentQueryRepository
    {
        private readonly OEPWriteDB _context; /// <summary>
        /// read db cevrilmelidir
        /// </summary>
        private readonly DbSet<Student> _entities;

        public StudentQueryRepository(OEPWriteDB context) : base(context)
        {
            _context = context;
            _entities = context.Set<Student>();
        }

        public Task<StudentResponseDTO> GetStudentById(int id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<List<StudentResponseDTO>> GetStudents(CancellationToken cancellationToken)
        {
            var students= await  _entities.ToListAsync(cancellationToken);

            var studentDtos = students.Select(s => new StudentResponseDTO
            {
                Id = s.ID,
                Name = s.Name,
                LastName= s.LastName,
                PIN = s.PIN                
            }).ToList();

            return studentDtos;
        }
    }
}
