using NeuTournament.Application.Exceptions;
using System.Net;
using System.Text.Json;

namespace NeuTournament.API.CustomExceptionMiddleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            this.logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                var response = context.Response;
                response.ContentType = "application/json";

                switch (error)
                {
                    case RecordNotFoundException ex:
                        // custom application error
                        logger.LogError("The information passed is not valid. More details : ",ex);
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        break;
                    case KeyNotFoundException ex:
                        // not found error
                        logger.LogError("Cannot fetch the details from the given ID. More details :", ex);
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        break;
                    case ArgumentNullException ex:
                        // custom application error
                        logger.LogError("Argument passed cannot be null. More details :", ex);
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        break;
                    case ArgumentOutOfRangeException ex:
                        logger.LogError("Argument passed is out of range. More details :", ex);
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        break;
                    case UnauthorizedAccessException ex:
                        logger.LogError("Not authorized for this request. More details :", ex);
                        response.StatusCode = (int)HttpStatusCode.Unauthorized;
                        break;
                    case MethodNotAllowed ex:
                        logger.LogError("This method is not allowed. More details :", ex);
                        response.StatusCode = (int)HttpStatusCode.MethodNotAllowed;
                        break;
                    default:
                        // unhandled error
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        logger.LogError($"Something went wrong",error);
                        break;
                }
                var result = JsonSerializer.Serialize(new { Error_Message = error?.Message });
                await response.WriteAsync(result);
            }
        }
    }
}
