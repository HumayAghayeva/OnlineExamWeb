using Domain.DTOs.Read;
using Domain.DTOs.Write;
using System;
using System.Collections.Generic;
using FluentValidation.Results;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entity;
using Microsoft.Owin;

namespace Abstraction.Command
{
    public interface IStudentCommandRepository 
    {
        Task<ResponseDto> AddStudent(StudentRequestDto studentRequestDto,  
                      CancellationToken cancellationToken);

        Task<StudentResponseDto> LoginStudent(StudentLoginDto studentLoginDto, CancellationToken cancellationToken);    

        Task<ResponseDto> AddStudentPhoto(StudentPhotoDto studentPhoto, CancellationToken cancellationToken);  

        Task<ResponseDto> ConfirmStudent(int studentId, CancellationToken cancellationToken);  
    }
}
