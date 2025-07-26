using Abstraction.PaymentApi.Interfaces.CardOperations;
using Domain.Contract;
using Domain.Enums;
using OnlineExamPaymentAPI.Dtos.Request;
using OnlineExamPaymentAPI.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace OnlineExamPaymentAPI.Services.PaymentApiServices
{
    public class CardValidatorServices : ICardValidator
    {
        public async Task<ApiResponse<bool>> ValidateCardTypeAsync(PlasticCardDto cardDto, CancellationToken cancellationToken)
        {
            var response = new ApiResponse<bool>(false);

            if (string.IsNullOrWhiteSpace(cardDto.CardNumber))
            {
                response.Code = ResponseCode.ValidationError;
                response.Message = "Card number is required.";
                return response;
            }

            var sanitized = cardDto.CardNumber.Replace(" ", "").Trim();

            if (!Regex.IsMatch(sanitized, @"^\d{13,19}$"))
            {
                response.Code = ResponseCode.ValidationError;
                response.Message = "Card number must contain only digits (13–19 characters).";
                return response;
            }

            if (!await PlasticCardValidatorHelper.IsValidLuhn(sanitized))
            {
                response.Code = ResponseCode.ValidationError;
                response.Message = "Invalid card number (Luhn check failed).";
                return response;
            }

            if (!await PlasticCardValidatorHelper.IsSupportedCardType(sanitized))
            {
                response.Code = ResponseCode.ValidationError;
                response.Message = "Unsupported card type. Only Visa and MasterCard are accepted.";
                return response;
            }

            response.Data = true;
            response.Code = ResponseCode.Success;
            response.Message = "Card type is valid.";
            return response;
        }


        public Task<ApiResponse<bool>> ValidateCvvAsync(PlasticCardDto cardDto, CancellationToken cancellationToken)
        {
            var response = new ApiResponse<bool>(false);

            if (string.IsNullOrWhiteSpace(cardDto.CVV))
            {
                response.Code = ResponseCode.ValidationError;
                response.Message = "CVV is required.";
                return Task.FromResult(response);
            }

            if (!Regex.IsMatch(cardDto.CVV, @"^\d{3,4}$"))
            {
                response.Code = ResponseCode.ValidationError;
                response.Message = "CVV must be 3 or 4 digits.";
                return Task.FromResult(response);
            }

            response.Data = true;
            response.Code = ResponseCode.Success;
            response.Message = "CVV is valid.";
            return Task.FromResult(response);
        }

        public Task<ApiResponse<bool>> ValidateExpirationDateAsync(PlasticCardDto cardDto, CancellationToken cancellationToken)
        {
            var response = new ApiResponse<bool>(false);

            try
            {
                int month = Convert.ToInt32(cardDto.ExpireMonth);
                int year = Convert.ToInt32(cardDto.ExpireYear);

                if (month < 1 || month > 12)
                {
                    response.Code = ResponseCode.ValidationError;
                    response.Message = "Invalid expiration month.";
                    return Task.FromResult(response);
                }

                var now = DateTime.UtcNow;
                var expiry = new DateTime(year, month, DateTime.DaysInMonth(year, month), 23, 59, 59);

                if (expiry < now)
                {
                    response.Code = ResponseCode.ValidationError;
                    response.Message = "The card is expired.";
                    return Task.FromResult(response);
                }

                response.Data = true;
                response.Code = ResponseCode.Success;
                response.Message = "Expiration date is valid.";
                return Task.FromResult(response);
            }
            catch
            {
                response.Code = ResponseCode.ValidationError;
                response.Message = "Invalid expiration date format.";
                return Task.FromResult(response);
            }
        }

    }
}
