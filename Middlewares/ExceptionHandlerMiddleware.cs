using Erfa.IdentityService.ViewModels.Error;
using System.Net;
using System.Text.Json;

namespace Erfa.IdentityService.Middlewares
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private ILogger<ExceptionHandlerMiddleware> _logger { get; }

        public ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await ConvertException(context, ex);
            }
        }

        private Task ConvertException(HttpContext context, Exception exception)
        {
            HttpStatusCode httpStatusCode = HttpStatusCode.InternalServerError;

            context.Response.ContentType = "application/json";

            var result = string.Empty;
            _logger.LogError(exception, exception.Message);

            switch (exception)
            {

                default:
                    httpStatusCode = HttpStatusCode.InternalServerError;
                    result = JsonSerializer.Serialize(new ErrorResponse
                    {
                        IsSuccess = false,
                        StatusCode = 500,
                        Message = "Internal server error"
                    });
                    break;
            }

            _logger.LogError(result, exception);

            context.Response.StatusCode = (int)httpStatusCode;

            _logger.LogInformation($"Caught Exception {exception.GetType()} with messege: {exception.Message}");

            _logger.LogInformation($"Response: {result}, status code: {(int)httpStatusCode}");

            if (result == string.Empty)
            {
                result = JsonSerializer.Serialize(new ErrorResponse
                {
                    IsSuccess = false,
                    StatusCode = 500,
                    Message = "Internal server error"
                });
            }

            return context.Response.WriteAsync(result);
        }
    }
}