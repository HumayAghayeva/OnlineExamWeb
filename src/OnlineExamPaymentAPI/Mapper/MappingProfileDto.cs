using AutoMapper;
using OnlineExamPaymentAPI.Dtos.Request;
using OnlineExamPaymentAPI.Entity;

namespace OnlineExamPaymentAPI.Mapper
{
    public class MappingProfileDto : Profile
    {
        public MappingProfileDto()
        {
            CreateMap<PlasticCardDto , PlasticCards>(); 
            CreateMap< UserPlasticCardDto ,UserPlasticCard > (); 
        }
    }
}