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
using MongoDB.Driver;

namespace Business.Repositories
{
    public class StudentQueryRepository : Repository<StudentRequestDto>, IStudentQueryRepository
    {
        private readonly OEPWriteDB _context; /// <summary>
        /// </summary>
        private readonly DbSet<Student> _entities;
        private readonly IMongoCollection<StudentResponseDto> _studentResponseCollections;

        public StudentQueryRepository(OEPWriteDB context, IMongoCollection<StudentResponseDto> studentResponseCollections) : base(context)
        {
            _context = context;
            _entities = context.Set<Student>();
            _studentResponseCollections = studentResponseCollections;   
        }
        public async Task<List<StudentResponseDto>> GetStudents(CancellationToken cancellationToken)
        {
            return await _studentResponseCollections.Find(_ => true).ToListAsync();
        }
        public async Task<StudentResponseDto> GetStudentById(int id, CancellationToken cancellationToken)
        {
            var student = await _entities.FirstOrDefaultAsync(s => s.ID == id, cancellationToken);

            if (student == null)
                return null;

            return new StudentResponseDto
            {
                WriteDBId = student.ID.ToString(),
                Name = student.Name,
                LastName = student.LastName,
                Email = student.Email,
                PIN= student.PIN
            };
        }
    }
}
