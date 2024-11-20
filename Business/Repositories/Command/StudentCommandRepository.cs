using Domain.DTOs.Write;
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
using Domain.DTOs.Read;
using System.ComponentModel.DataAnnotations;
using Domain.Enums;

namespace Business.Repositories.Command
{
    public class StudentCommandRepository : Repository<StudentRequestDTO>,IStudentCommandRepository
    {
        private readonly OEPWriteDB _context;
        private UnitOfWork _unitOfWork; 
       
        private readonly DbSet<Student> _entities;

        public StudentCommandRepository(OEPWriteDB context) : base(context)
        {
            _context = context;
            _entities = context.Set<Student>();
        }

        public async Task AddStudent(StudentRequestDTO studentReadDTO, CancellationToken cancellationToken)
        {
        
            var studentEntity = new Student
            {
                Name = studentReadDTO.Name,
                LastName = studentReadDTO.LastName,
                DateOfBirth = studentReadDTO.DateOfBirth,
                PIN = studentReadDTO.PIN,
                GroupId =(int)studentReadDTO.GroupId,
                Email = studentReadDTO.Email,
                Password = studentReadDTO.Password,
                ConfirmPassword = studentReadDTO.ConfirmPassword
            };

            await _entities.AddAsync(studentEntity, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken); 

        }
        public async Task<StudentResponseDTO> LoginStudent(StudentLoginDTO studentLoginDTO, CancellationToken cancellationToken)
        {
            var student = await _context.Students.Where(w => w.Email == studentLoginDTO.Email &&
                             w.Password == studentLoginDTO.Password).FirstOrDefaultAsync(cancellationToken);
            if (student == null)
            {
                throw new Exception("Invalid email or password."); 
            }

            var studentResponseDTO = new StudentResponseDTO
            {
                Id = student.ID,
                Name = student.Name,
                Email = student.Email,
                PIN = student.Email,
                GroupName = Enum.GetName(typeof(Groups), student.GroupId)
            };

            return studentResponseDTO;  
        }

    }
}
