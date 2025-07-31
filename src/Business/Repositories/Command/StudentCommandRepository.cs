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
using Domain.Dtos.Write;
using AutoMapper;
using Abstraction;

namespace Business.Repositories.Command
{
    public class StudentCommandRepository : Repository<StudentRequestDto>, IStudentCommandRepository
    {
        private readonly OEPWriteDB _context;

        private readonly DbSet<Student> _studentEntity;
        private readonly DbSet<StudentPhoto> _studentPhotos;
        private readonly DbSet<StudentRoles> _studentRoles;
        private readonly IMapper _mapper;

        public StudentCommandRepository(OEPWriteDB context , IMapper mapper) : base(context)
        {
            _context = context;
            _studentEntity = context.Set<Student>();
            _studentPhotos = context.Set<StudentPhoto>();
            _studentRoles = context.Set<StudentRoles>();
            _mapper = mapper;
        }

        #region LoginStudent
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
        #endregion

        #region AddStudentPhoto
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

        #endregion

        #region AddStudent
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
        #endregion

        #region AssignRoleToStudentAsync
        public async Task<ResponseDto> AssignRoleToStudentAsync(StudentRolesDto studentRolesDto, CancellationToken cancellationToken)
        {
            var studentRoledto = new StudentRolesDto
            {
                StudentId = studentRolesDto.StudentId,
                RoleId = studentRolesDto.RoleId,
                CreateDate = studentRolesDto.CreateDate
            };

            var studentRole = _mapper.Map<StudentRoles>(studentRolesDto);

            _studentRoles.Add(studentRole);

            await _context.SaveChangesAsync(cancellationToken);

            return new ResponseDto
            {
                Success = true,
                Message = "Student  Role was added successfully.",
                Id = studentRole.Id
            };
        }
        #endregion

    
        #region IsRoleAlreadyAssignedAsync
        //public async Task<ResponseDto> AssignRoleIfNotExistsAsync(StudentRolesDto dto, CancellationToken cancellationToken)
        //{
          
        //    var isAlreadyAssigned = await _studentRoles.IsRoleAssignedAsync(dto.StudentId, dto.RoleId, cancellationToken);

        //    if (isAlreadyAssigned)
        //    {
        //        return new ResponseDto
        //        {
        //            Success = false,
        //            Message = "Student already has this role assigned. No action taken."
        //        };
        //    }

         
        //    return await _repository.AddStudentRoleAsync(dto, cancellationToken);
        //}
        #endregion

    }
   
}

      

