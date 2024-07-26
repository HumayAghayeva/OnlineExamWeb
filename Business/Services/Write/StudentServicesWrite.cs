using Abstraction.Write;
using Abstraction;
using Domain.DTOs.Write;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;
using Domain.Enums;

namespace Business.Services.Write
{
    public class StudentServicesWrite : IStudentPostRepository
    {
        private readonly IUnitOfWork uow;
        public StudentServicesWrite(IUnitOfWork _uow)
        {
            uow = _uow;
        }
        public async Task PostStudentAsync(StudentRequestDTO studentRequestDTO, CancellationToken token)
        {
            try
            {
                await uow.BeginTransactionAsync();

                var student = new Student
                {
                    Name = studentRequestDTO.Name,
                    LastName = studentRequestDTO.LastName,
                    DateOfBirth = studentRequestDTO.DateOfBirth,
                    GroupId = studentRequestDTO.GroupId
                };

                var studentRepository = uow.Repository<Student>();
                studentRepository.Add(student);

                uow.Save();
                uow.Commit();
            }
            catch (Exception ex)
            {
                uow.Rollback();
                // Handle the exception (logging, rethrowing, etc.)
            }
        }
    }
}   
