using Domain.DTOs.Read;
using Domain.DTOs.Write;
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
using System.ComponentModel.DataAnnotations;
using Domain.Enums;
using System.Threading;
using Microsoft.Owin;
using Domain.Entity.Write;

namespace Business.Repositories.Command
{
    public class StudentCommandRepository : Repository<StudentRequestDTO>, IStudentCommandRepository
    {
        private readonly OEPWriteDB _context;

        private readonly DbSet<Student> _studentEntity;
        private readonly DbSet<StudentPhoto> _studentPhotos;

        public StudentCommandRepository(OEPWriteDB context) : base(context)
        {
            _context = context;
            _studentEntity = context.Set<Student>();
            _studentPhotos = context.Set<StudentPhoto>();
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
                //Id = student.ID,
                Name = student.Name,
                LastName=student.LastName,
                Email = student.Email,
                PIN = student.Email,
                GroupName = Enum.GetName(typeof(Groups), student.GroupId)
            };

            return studentResponseDTO;
        }

        public async Task<ResponseDTO> AddStudentPhoto(StudentPhotoDTO studentPhotoDTO, CancellationToken cancellationToken)
        {

            var studentPhoto = new StudentPhoto
            {
                StudentId = studentPhotoDTO.StudentId,
                FileName = studentPhotoDTO.FileName,
                PhotoPath = studentPhotoDTO.PhotoPath
            };

            await _studentPhotos.AddAsync(studentPhoto, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);

            var response = new ResponseDTO
            {
                Id = studentPhoto.Id,
                Success =true,
                Message="Added successfully!"
            };

           return response;
        }

        public async Task<ResponseDTO> ConfirmStudent(int studentId, CancellationToken cancellationToken)
        {
          
            var student = await _context.Students
                .Where(w => w.ID == studentId)
                .FirstOrDefaultAsync(cancellationToken);

            if (student == null)
            {
                throw new Exception("Invalid data.");
            }

            student.IsConfirmed = true;
          
            _context.Students.Update(student); 

            await _context.SaveChangesAsync(cancellationToken);
          
            return new ResponseDTO
            {
                Success = true,
                Message = "Student was confirmed successfully.",
                Id = student.ID
            };
        }
        public async Task<ResponseDTO> AddStudent(StudentRequestDTO studentReadDTO, CancellationToken cancellationToken)
        {
            try
            {
                var studentEntity = new Student
                {
                    Name = studentReadDTO.Name,
                    LastName = studentReadDTO.LastName,
                    DateOfBirth = studentReadDTO.DateOfBirth,
                    PIN = studentReadDTO.PIN,
                    GroupId = (int)studentReadDTO.GroupId,
                    Email = studentReadDTO.Email,
                    Password = studentReadDTO.Password,
                    ConfirmPassword = studentReadDTO.ConfirmPassword
                };

                await _studentEntity.AddAsync(studentEntity, cancellationToken);



                await _context.SaveChangesAsync(cancellationToken);

                return new ResponseDTO
                {
                    Success = true,
                    Message = "Student was added successfully.",
                    Id = studentEntity.ID
                };
            }
            catch (Exception ex)
            {
                {
                    return new ResponseDTO
                    {
                        Success = false,
                        Message = ex.Message,
                        Id = 0
                    };
                }
            }

        }
    }
}

