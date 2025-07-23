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
    public class StudentCommandRepository : Repository<StudentRequestDto>, IStudentCommandRepository
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

        public async Task<StudentResponseDto> LoginStudent(StudentLoginDto studentLoginDto, CancellationToken cancellationToken)
        {
            var student = await _context.Students.Where(w => w.Email == studentLoginDto.Email &&
                             w.Password == studentLoginDto.Password).FirstOrDefaultAsync(cancellationToken);

            if (student == null)
            {
                throw new Exception("Invalid email or password.");
            }

            var studentResponseDto = new StudentResponseDto
            {
                WriteDBId = student.ID.ToString(),
                Name = student.Name,
                LastName=student.LastName,
                Email = student.Email,
                PIN = student.Email,
                GroupName = Enum.GetName(typeof(Groups), student.GroupId)
            };

            return studentResponseDto;
        }

        public async Task<ResponseDto> AddStudentPhoto(StudentPhotoDto studentPhotoDto, CancellationToken cancellationToken)
        {

            var studentPhoto = new StudentPhoto
            {
                StudentId = studentPhotoDto.StudentId,
                FileName = studentPhotoDto.FileName,
                PhotoPath = studentPhotoDto.PhotoPath
            };

            await _studentPhotos.AddAsync(studentPhoto, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);

            var response = new ResponseDto
            {
                Id = studentPhoto.Id,
                Success =true,
                Message="Added successfully!"
            };

           return response;
        }

       
        public async Task<ResponseDto> AddStudent(StudentRequestDto studentReadDto, CancellationToken cancellationToken)
        {
            try
            {
                var studentEntity = new Student
                {
                    Name = studentReadDto.Name,
                    LastName = studentReadDto.LastName,
                    DateOfBirth = studentReadDto.DateOfBirth,
                    PIN = studentReadDto.PIN,
                    Email = studentReadDto.Email,
                    Password = studentReadDto.Password,
                    ConfirmPassword = studentReadDto.ConfirmPassword
                };

                await _studentEntity.AddAsync(studentEntity, cancellationToken);



                await _context.SaveChangesAsync(cancellationToken);

                return new ResponseDto
                {
                    Success = true,
                    Message = "Student was added successfully.",
                    Id = studentEntity.ID
                };
            }
            catch (Exception ex)
            {
                {
                    return new ResponseDto
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

