using Domain.Contract;
using OnlineExamPaymentAPI.Dtos.Request;

namespace OnlineExamPaymentAPI.Interfaces
{
    public interface IPlasticCardServices
    {
        Task<ApiResponse> CreatePlasticCardAsync(PlasticCardDto plasticCardDto, CancellationToken cancellationToken);
    }
}
