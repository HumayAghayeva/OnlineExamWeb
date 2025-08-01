using Microsoft.AspNetCore.Mvc;
using OnlineExamPaymentAPI.Dtos.Request;
using System.Net.Http;

namespace OnlineExamWeb.Controllers
{
    public class CardOperationsController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        private readonly IConfiguration _configuration;

        public CardOperationsController(IHttpClientFactory httpClientFactory , IConfiguration configuration )
        {
            _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException((nameof(httpClientFactory)));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
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

            if (plasticCardDto == null)
            {
                throw new Exception("Plastic Card is null");
            }

            if (!ModelState.IsValid)
            {
                return View(plasticCardDto);
            }

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

            //   var result = await response.Content.ReadFromJsonAsync<ValidationResultDto>(cancellationToken: cancellationToken);

            //if (result?.IsValid != true)
            //{
            //    ModelState.AddModelError("", result?.Message ?? "Invalid card.");
            //    return View(plasticCardDto);
            //}

            return View("Success"); // Or your next step
        }

    }
}
