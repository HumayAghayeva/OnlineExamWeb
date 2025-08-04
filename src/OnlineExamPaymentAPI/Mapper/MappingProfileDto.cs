using AutoMapper;
using Domain.Dtos.Write;
using Domain.DTOs.Write;
using Domain.Entity;
using OnlineExamPaymentAPI.Dtos.Request;
using OnlineExamPaymentAPI.Entity;

namespace OnlineExamPaymentAPI.Mapper
{
    public class MappingProfileDto : Profile
    {
        public MappingProfileDto()
        {
            CreateMap<PlasticCardDto, PlasticCards>();
            CreateMap<StudentQuizzesDto, StudentQuizzes>();
        }
    }
}