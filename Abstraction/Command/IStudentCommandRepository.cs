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
        Task<ResponseDTO> AddStudent(StudentRequestDTO studentRequestDTO,  
                      CancellationToken cancellationToken);

        Task<StudentResponseDTO> LoginStudent(StudentLoginDTO studentLoginDTO, CancellationToken cancellationToken);    

        Task<ResponseDTO> AddStudentPhoto(StudentPhotoDTO studentPhoto, CancellationToken cancellationToken);  

        Task<ResponseDTO> ConfirmStudent(int studentId, CancellationToken cancellationToken);  
    }
}
