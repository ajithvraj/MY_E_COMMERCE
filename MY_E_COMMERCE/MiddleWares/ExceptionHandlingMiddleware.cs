using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Text.Json;
using Microsoft.Data.SqlClient;


namespace MY_E_COMMERCE.MiddleWares
{
    public class ExceptionHandlingMiddleware
    {

        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;


        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;



        }


        public async Task Invoke(HttpContext context)

        {

            try
            {
                await _next(context); //used for proceeding to the next middleware / controller

            }

            catch (Exception ex) 
                {

                _logger.LogError(ex, "An unhandled exception occurred.");

                await HandleExceptionAsync(context, ex);

                }

        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {

            HttpStatusCode status;
            string message;

            switch (exception)
            {

                case UnauthorizedAccessException:
                    status = HttpStatusCode.Unauthorized;
                    message = exception.Message;
                    break;

                case SqlException:
                    status = HttpStatusCode.InternalServerError;
                    message = "A database base occured";
                    break;

                case ArgumentException:
                    status = HttpStatusCode.BadRequest;
                    message = exception.Message;
                    break;

                default:
                    status = HttpStatusCode.InternalServerError;
                    message = exception.Message;
                    break;

            }

            var response = new
            {

                status = (int)status,
                Error = message,
                Exceptiontype = exception.GetType().Name 


            };

            var payload = JsonSerializer.Serialize(response);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)status;

            return context.Response.WriteAsync(payload);
        }

    }

}
