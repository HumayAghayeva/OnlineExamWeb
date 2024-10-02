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

namespace Business.Repositories.Command
{
    public class StudentCommandRepository : Repository<StudentRequestDTO>,IStudentCommandRepository
    {
        private readonly OEPWriteDB _context;
        //private readonly UnitOfWork _unitOfWork;
        private readonly DbSet<Student> _entities;

        public StudentCommandRepository(OEPWriteDB context) : base(context)
        {
            _context = context;
            _entities = context.Set<Student>();
          //  _unitOfWork = unitOfWork;
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

            //await _unitOfWork.SaveAsync(cancellationToken);
        }

       
    }
}
