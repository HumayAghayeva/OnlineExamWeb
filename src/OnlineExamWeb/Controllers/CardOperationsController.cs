using Abstraction.Interfaces;
using Domain.Contract;
using Domain.Enums;
using Microsoft.AspNetCore.Mvc;
using OnlineExamPaymentAPI.Dtos.Request;
using OnlineExamPaymentAPI.Interfaces;
using System.Net.Http;

namespace OnlineExamWeb.Controllers
{
    public class CardOperationsController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IJwtTokenService _jwtTokenService;
        private readonly IPlasticCardServices _plasticCardServices;
        private readonly IConfiguration _configuration;

        public CardOperationsController(IHttpClientFactory httpClientFactory,
            IConfiguration configuration,
            IJwtTokenService jwtTokenService,
            IPlasticCardServices plasticCardServices)
        {
            _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException((nameof(httpClientFactory)));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _jwtTokenService = jwtTokenService ?? throw new ArgumentNullException(nameof(jwtTokenService));
            _plasticCardServices = plasticCardServices ?? throw new ArgumentNullException(nameof(plasticCardServices));
        }

        public ActionResult CardInfo()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CardInfo(PlasticCardDto plasticCardDto, CancellationToken cancellationToken)
        {

            string validateType = _configuration["PaymentApi:ValidateType"];
            string validateCVV = _configuration["PaymentApi:ValidateCVV"];
            string validateExpirey = _configuration["PaymentApi:ValidateExpiry"];

            #region CheckValidation of Card
            if (plasticCardDto == null)
            {
                throw new Exception("Plastic Card is null");
            }

            //if (!ModelState.IsValid)
            //{
            //    return View(plasticCardDto);
            //}

            var client = _httpClientFactory.CreateClient();

            var cardTypeResponse = await client.PostAsJsonAsync(validateType, plasticCardDto, cancellationToken);

            if (!cardTypeResponse.IsSuccessStatusCode)
            {
                ModelState.AddModelError("", "Card Type validation failed.");
                return View(plasticCardDto);
            }

            var cardCVVResponse = await client.PostAsJsonAsync(validateCVV, plasticCardDto, cancellationToken);

            if (!cardTypeResponse.IsSuccessStatusCode)
            {
                ModelState.AddModelError("", "Card Type validation failed.");
                return View(plasticCardDto);
            }

            var cardExpireResponse = await client.PostAsJsonAsync(validateExpirey, plasticCardDto, cancellationToken);

            if (!cardExpireResponse.IsSuccessStatusCode)
            {
                ModelState.AddModelError("", "Card Type validation failed.");
                return View(plasticCardDto);
            }
            #endregion

            #region Add Table Dto's 
            var plasticCard = new PlasticCardDto
            {
                CardNumber = plasticCardDto.CardNumber,
                CardType = plasticCardDto.CardType,
                CVV = plasticCardDto.CVV,
                ExpireMonth = plasticCardDto.ExpireMonth,
                HolderName = plasticCardDto.HolderName,
                ExpireYear = plasticCardDto.ExpireYear
            };

            var resultPlasticCard = await _plasticCardServices.CreatePlasticCardAsync(plasticCardDto, cancellationToken);

            if (resultPlasticCard.Code == ResponseCode.Success)
            {
                var studentId = HttpContext.Session.GetInt32("StudentId");
                var userPlasticCardDto = new UserPlasticCardDto
                {
                    UserID = studentId.HasValue ? studentId.Value : 0,
                    PlasticCardID = resultPlasticCard.Data?.PlasticCardId.HasValue == true
                     ? resultPlasticCard.Data.PlasticCardId.Value
                     : 0
                };

                var resultUserPlasticCard = await _plasticCardServices.CreateUserPlasticCardAsync(userPlasticCardDto, cancellationToken);

                if (resultUserPlasticCard.Code != ResponseCode.Success)
                {
                    ModelState.AddModelError("", "Failed to assign plastic card to user.");
                    return View(plasticCardDto);
                }


            }
            return View(plasticCardDto);    // quiz baslamalidir.
            #endregion
        }
    }
}

