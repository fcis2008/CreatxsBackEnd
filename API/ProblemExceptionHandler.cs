using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace API
{
    public class ProblemExceptionHandler : IExceptionHandler
    {
        private readonly IProblemDetailsService _problemDetailsService;
        private readonly ILogger<ProblemExceptionHandler> _logger;

        public ProblemExceptionHandler(IProblemDetailsService problemDetailsService, ILogger<ProblemExceptionHandler> logger)
        {
            _problemDetailsService = problemDetailsService;
            _logger = logger;
        }

        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            var traceId = Activity.Current?.Id ?? httpContext.TraceIdentifier;

            _logger.LogError(exception, "An unhandled exception occurred while processing the request on machine {MachineName}. TraceId: {traceId}",
                Environment.MachineName, traceId);

            var (statusCode, title) = MapException(exception);

            _logger.LogError("Error Message: {exceptionMessage}, Time of occurrence {time}", exception.Message, DateTime.UtcNow);

            //var problemDetails = new ProblemDetails
            //{
            //    Detail = exception.Message,
            //    Status = statusCode,
            //    Title = title,
            //    Type = exception.GetType().Name,
            //};
            //await _problemDetailsService.WriteAsync(new ProblemDetailsContext { HttpContext = httpContext, Exception = exception, ProblemDetails = problemDetails });
            //await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

            //return true;

            await Results.Problem(
                statusCode: statusCode,
                title: title,
                detail: exception.Message,
                instance: $"{httpContext.Request.Method} {httpContext.Request.Path}",
                extensions: new Dictionary<string, object>
                {
                    ["traceId"] = traceId,
                    ["requestId"] = httpContext.TraceIdentifier
                }).ExecuteAsync(httpContext);

            return true;
        }

        private (int statusCode, string title) MapException(Exception exception)
        {
            return exception switch
            {
                ArgumentOutOfRangeException => (StatusCodes.Status400BadRequest, exception.Message),
                _ => (StatusCodes.Status500InternalServerError, "An unexpected error occurred.")
            };
        }
    }
}
