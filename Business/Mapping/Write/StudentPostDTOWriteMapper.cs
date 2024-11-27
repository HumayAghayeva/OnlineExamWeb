using AutoMapper;
using Domain.DTOs.Write;
using Domain.Entity.Write;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Mapping.Write
{
    public class StudentPostDTOWriteMapper : Profile
    {
        public StudentPostDTOWriteMapper()
        {
            CreateMap<StudentRequestDTO, Student>();
        }
    }
}
