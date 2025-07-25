using Domain.Contract;
using Domain.Enums;
using System.Net;
using System.Text.Json;

namespace OnlineExamWeb.Middleware
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }


        private static Task HandleExceptionAsync(HttpContext context, Exception error)
        {
            var response = context.Response;
            response.ContentType = "application/json";
            response.StatusCode = (int)HttpStatusCode.InternalServerError;

            ApiResponse apiResponse = new ApiResponse();
            if(error is GlobalException)
            {
                GlobalException exception = (GlobalException)error;
                apiResponse.Code = exception.Code; 
                apiResponse.Message = exception.Message;
            }
            else
            {
                apiResponse.Code = ResponseCode.InternalServerError;
                apiResponse.Message = error.Message;    
            }
            var result = JsonSerializer.Serialize(apiResponse, new JsonSerializerOptions
            {
                PropertyNamingPolicy =JsonNamingPolicy.CamelCase,
                DefaultIgnoreCondition=System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
            });

            return response.WriteAsync(result);
        }
    }
}
