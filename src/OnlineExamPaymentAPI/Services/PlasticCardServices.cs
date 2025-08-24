using AutoMapper;
using Domain.Contract;
using Domain.Entity;
using Domain.Enums;
using OnlineExamPaymentAPI.DbConn;
using OnlineExamPaymentAPI.Dtos.Request;
using OnlineExamPaymentAPI.Dtos.Response;
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

        public async Task<ApiResponse<PlasticCardResponseDto>> CreatePlasticCardAsync(PlasticCardDto plasticCard, CancellationToken cancellationToken)
        {
            try
            {
                var plasticCardEntity = new PlasticCards
                {
                    ID = plasticCard.ID,
                    HolderName = plasticCard.HolderName,
                    CardNumber = plasticCard.CardNumber,
                    ExpireMonth = plasticCard.ExpireMonth,
                    ExpireYear = plasticCard.ExpireYear,
                    CVV = plasticCard.CVV,
                    CardType = plasticCard.CardType
                };


                await _dbContext.PlasticCards.AddAsync(plasticCardEntity, cancellationToken);
                await _dbContext.SaveChangesAsync(cancellationToken);

             
                return new ApiResponse<PlasticCardResponseDto>(new PlasticCardResponseDto
                {
                    PlasticCardId = plasticCardEntity.ID
                })
                {
                    Code = ResponseCode.Success,
                    Message = "Plastic card was added successfully."
                };
            }
            catch (Exception ex)
            {
             
                return new ApiResponse<PlasticCardResponseDto>(new PlasticCardResponseDto
                {
                    PlasticCardId =null,
                })
                {
                    Code = ResponseCode.InternalServerError,
                    Message = "Plastic card was added successfully."
                };
            }
        }


        public async Task<ApiResponse> CreateUserPlasticCardAsync(UserPlasticCardDto userPlasticCardDto, CancellationToken cancellationToken)
        {
            try
            {
                var userPlasticCardEntity = new UserPlasticCard
                {
                    UserId = userPlasticCardDto.UserID,
                    PlasticCardID = userPlasticCardDto.PlasticCardID
                };
                await _dbContext.UserPlasticCards.AddAsync(userPlasticCardEntity, cancellationToken);
                await _dbContext.SaveChangesAsync(cancellationToken);

                return new ApiResponse
                {
                    Code = ResponseCode.Success,
                    Message = "Plastic card was added successfully."
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse
                {
                    Code = ResponseCode.InternalServerError,
                    Message = "Plastic card was added successfully."
                };
            }
        }
    }
}
