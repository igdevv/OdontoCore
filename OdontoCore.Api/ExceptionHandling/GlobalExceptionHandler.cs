using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using OdontoCore.Application.Exceptions;

namespace OdontoCore.Api.ExceptionHandling
{
    public sealed class GlobalExceptionHandler : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            int statusCode;
            string title;

            switch (exception)
            {
                case AppointmentConflictException:
                    statusCode = StatusCodes.Status409Conflict;
                    title = "Appointment conflict";
                    break;

                case ResourceNotFoundException:
                    statusCode = StatusCodes.Status404NotFound;
                    title = "Resource not found";
                    break;

                default:
                    return false;
            }

            var problem = new ProblemDetails
            {
                Status = statusCode,
                Title = title,
                Detail = exception.Message
            };

            httpContext.Response.StatusCode = statusCode;
            await httpContext.Response.WriteAsJsonAsync(problem, cancellationToken);

            return true;
        }
    }
}
