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
using Domain.DTOs.Read;
using Azure.Core;
using static Azure.Core.HttpHeader;
using AutoMapper;
using FluentValidation.Results;

namespace Business.Services.Write
{
    public class StudentServicesWrite : IStudentPostRepository
    {
        private readonly IUnitOfWork uow;
        private readonly IMapper mapper;
        public StudentServicesWrite(IUnitOfWork _uow,IMapper _mapper)
        {
              uow = _uow ?? throw new ArgumentNullException(nameof(_uow));
              mapper = _mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<ValidationResult> PostStudentAsync(StudentRequestDTO studentRequestDTO, CancellationToken token)
        {
            try
            {
                await uow.BeginTransactionAsync(IsolationLevel.ReadCommitted, token);

                var student = mapper.Map<Student>(studentRequestDTO);

                var studentRepository = uow.Repository<Student>();

                studentRepository.Add(student);

                await uow.SaveAsync(token);

                await uow.CommitAsync(token);

                return Helper.GenerateValidation.GenerateValidationResult(true, "Student created successfully", null);
            }
            catch (Exception ex)
            {
                await uow.RollbackAsync(token);

                return Helper.GenerateValidation.GenerateValidationResult(true, "Student created successfully", null);
            }
        }

        
       public async Task<StudentReadDTO> GetStudentById(int id, CancellationToken token)
        {
            var studentRepository =  uow.Repository<Student>();       

            var student= await studentRepository.GetByIdAsync(id);

            var studentResponse = mapper.Map<StudentReadDTO>(student);

            if (studentResponse == null)
            {
                return  null;
            }

            return studentResponse;

        }
        public async Task UpdateStudentAsync(int id, StudentRequestDTO studentRequest, CancellationToken token)
        {
            try
            {
                await uow.BeginTransactionAsync(IsolationLevel.ReadCommitted, token);

                var studentRepository = uow.Repository<Student>();

                var existingStudent = await studentRepository.GetByIdAsync(id);

                if (existingStudent == null)
                {
                    throw new KeyNotFoundException($"Student with id {id} not found.");
                }

              
                mapper.Map(studentRequest, existingStudent);

                studentRepository.Update(existingStudent);

                await uow.SaveAsync(token);
                await uow.CommitAsync(token);
            }
            catch (Exception ex)
            {
                await uow.RollbackAsync(token);

                throw;

            }
        }
    }
}
   
