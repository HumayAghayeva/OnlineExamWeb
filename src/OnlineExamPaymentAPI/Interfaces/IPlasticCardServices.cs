using Domain.Contract;
using OnlineExamPaymentAPI.Dtos.Request;
using OnlineExamPaymentAPI.Dtos.Response;

namespace OnlineExamPaymentAPI.Interfaces
{
    public interface IPlasticCardServices
    {
        Task<ApiResponse<PlasticCardResponseDto>> CreatePlasticCardAsync(PlasticCardDto plasticCardDto, CancellationToken cancellationToken);

        Task<ApiResponse> CreateUserPlasticCardAsync(UserPlasticCardDto userPlasticCardDto, CancellationToken cancellationToken);

        Task<bool> PlasticCardExistsAsync(PlasticCardDto userPlasticCardDto, CancellationToken cancellationToken = default);
    }
}
