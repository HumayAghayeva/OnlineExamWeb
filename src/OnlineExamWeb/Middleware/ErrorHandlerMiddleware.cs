using System.Net;

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


        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var response = context.Response;
            response.ContentType = "application/json";
            response.StatusCode = (int)HttpStatusCode.InternalServerError;

           
            var errorDetails = new
            {
                StatusCode = response.StatusCode,
                Message = "An unexpected error occurred. Please try again later.",
                Detail = exception.Message 
            };

            return response.WriteAsJsonAsync(errorDetails);
        }
    }
}
