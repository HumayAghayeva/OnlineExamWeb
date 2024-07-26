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
using System.Data;

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
                await uow.BeginTransactionAsync(IsolationLevel.ReadCommitted, token);

                var student = new Student
                {
                    Name = studentRequestDTO.Name,
                    LastName = studentRequestDTO.LastName,
                    DateOfBirth = studentRequestDTO.DateOfBirth,
                    GroupId = studentRequestDTO.GroupId
                };

                var studentRepository = uow.Repository<Student>();
                studentRepository.Add(student);

                await uow.SaveAsync(token);
                await uow.CommitAsync(token);
            }
            catch (Exception ex)
            {
                await uow.RollbackAsync(token);
                // Handle the exception (logging, rethrowing, etc.)
            }
        }

        public async void GetStudentById(int id, CancellationToken token)
        {

        }
    }
}
   
