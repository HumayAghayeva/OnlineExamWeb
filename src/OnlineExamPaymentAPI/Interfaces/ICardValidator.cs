using Domain.Contract;
using OnlineExamPaymentAPI.Dtos.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstraction.PaymentApi.Interfaces.CardOperations
{
    public interface ICardValidator
    {
        Task<ApiResponse<bool>> ValidateExpirationDateAsync(PlasticCardDto cardDto, CancellationToken cancellationToken);
        Task<ApiResponse<bool>> ValidateCardTypeAsync(PlasticCardDto cardDto, CancellationToken cancellationToken);
        Task<ApiResponse<bool>> ValidateCvvAsync(PlasticCardDto cardDto, CancellationToken cancellationToken);
    }
}
