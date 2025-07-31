using AutoMapper;
using Domain.Dtos.Write;
using Domain.DTOs.Write;
using Domain.Entity.Write;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Mapper
{
    public class MappingProfileDto : Profile
    {
        public MappingProfileDto()
        {
            CreateMap<StudentRequestDto, Student>();
            CreateMap<StudentRolesDto, StudentRoles>();
        }
    }
}
