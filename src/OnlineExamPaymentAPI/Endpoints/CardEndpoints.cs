using Abstraction.PaymentApi.Interfaces.CardOperations;
using OnlineExamPaymentAPI.Dtos.Request;

namespace OnlineExamPaymentAPI.Endpoints
{
    public static class CardEndpoints
    {
        public static void MapCardEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapPost("/api/card/validate-type", async (
                PlasticCardDto cardDto,
                ICardValidator cardValidator,
                CancellationToken cancellationToken) =>
            {
                var result = await cardValidator.ValidateCardTypeAsync(cardDto, cancellationToken);
                return Results.Ok(result);
            });

            app.MapPost("/api/card/validate-cvv", async (
                PlasticCardDto cardDto,
                ICardValidator cardValidator,
                CancellationToken cancellationToken) =>
            {
                var result = await cardValidator.ValidateCvvAsync(cardDto, cancellationToken);
                return Results.Ok(result);
            });

            app.MapPost("/api/card/validate-expiry", async (
                PlasticCardDto cardDto,
                ICardValidator cardValidator,
                CancellationToken cancellationToken) =>
            {
                var result = await cardValidator.ValidateExpirationDateAsync(cardDto, cancellationToken);
                return Results.Ok(result);
            });
        }
    }

}
