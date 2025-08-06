using AutoMapper;
using Domain.Contract;
using Domain.Dtos.Write;
using Domain.DTOs.Read;
using Domain.Entity;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using OnlineExamPaymentAPI.DbConn;
using OnlineExamPaymentAPI.Dtos.Request;
using OnlineExamPaymentAPI.Entity;
using OnlineExamPaymentAPI.Interfaces;

namespace OnlineExamPaymentAPI.Services
{
    public class PlasticCardServices : IPlasticCardServices
    {
        private readonly OnlineExamDbContext _dbContext;
        private readonly IMapper _mapper;

        public PlasticCardServices(OnlineExamDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<ApiResponse> CreatePlasticCardAsync(PlasticCardDto plasticCard, CancellationToken cancellationToken)
        {
            var plasticCardEntity = _mapper.Map<PlasticCards>(plasticCard);

            await _dbContext.PlasticCards.AddAsync(plasticCardEntity, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return new ApiResponse<int>(plasticCardEntity.ID)
            {
                Code = ResponseCode.Success,
                Message = "Plastic card was added successfully."
            };
        }
    }

}
