using Domain.Contract;
using Domain.Entity;
using Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using OnlineExamPaymentAPI.Dtos.Request;
using OnlineExamPaymentAPI.Services;

namespace OnlineExamPaymentAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    //[Authorize(Policy = nameof(Roles.QuizParticipant))]
    public class CardOperationController: ControllerBase
    {
        private readonly PlasticCardServices _plasticCard;
        //private readonly Serilog _serilog;

        public CardOperationController(PlasticCardServices plasticCard)
        {
            _plasticCard= plasticCard;
        }

        [HttpPost]
        public async Task<ApiResponse> PlasticCardInsert(PlasticCardDto cardDto , CancellationToken cancellationToken)
        {
            var result= await  _plasticCard.CreatePlasticCardAsync(cardDto , cancellationToken);   
            return result;  
        }
    }
}
