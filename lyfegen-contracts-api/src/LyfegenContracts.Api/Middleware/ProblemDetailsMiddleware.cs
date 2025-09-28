using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;

namespace LyfegenContracts.Api.Middleware
{
    public class ProblemDetailsMiddleware
    {
        private readonly RequestDelegate _next;

        public ProblemDetailsMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IProblemDetailsService problemDetailsService)
        {
            try
            {
                await _next(context);

                // Post-processing non-exception error status codes
                if (context.Response.StatusCode >= 400 && !context.Response.HasStarted)
                {
                    // If nothing was written, emit ProblemDetails
                    if (context.Response.ContentLength == null && string.IsNullOrEmpty(context.Response.ContentType))
                    {
                        var pd = CreateProblemDetails(
                            context.Response.StatusCode,
                            ReasonPhrases.GetReasonPhrase(context.Response.StatusCode),
                            null,
                            context.Request.Path,
                            context.TraceIdentifier);

                        context.Response.ContentType = "application/problem+json";
                        await problemDetailsService.WriteAsync(new ProblemDetailsContext
                        {
                            HttpContext = context,
                            ProblemDetails = pd
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                var status = ex switch
                {
                    ValidationException => StatusCodes.Status400BadRequest,
                    KeyNotFoundException => StatusCodes.Status404NotFound,
                    _ => StatusCodes.Status500InternalServerError
                };

                var title = status switch
                {
                    400 => "Validation Failed",
                    404 => "Resource Not Found",
                    _ => "Internal Server Error"
                };

                var detail = ex is ValidationException ? "One or more validation errors occurred." : ex.Message;

                var pd = CreateProblemDetails(status, title, detail, context.Request.Path, context.TraceIdentifier);

                if (ex is ValidationException fv)
                {
                    pd.Extensions["errors"] = fv.Errors
                        .GroupBy(e => e.PropertyName)
                        .ToDictionary(g => g.Key, g => g.Select(e => e.ErrorMessage).ToArray());
                }

                if (context.Response.HasStarted)
                {
                    // Too late to change status/body; just log and rethrow if you want default behavior.
                    throw;
                }

                context.Response.Clear(); // remove any partial content
                context.Response.StatusCode = status;
                context.Response.ContentType = "application/problem+json";

                await problemDetailsService.WriteAsync(new ProblemDetailsContext
                {
                    HttpContext = context,
                    ProblemDetails = pd,
                    Exception = ex
                });
            }
        }


        private static ProblemDetails CreateProblemDetails(
            int status,
            string title,
            string? detail,
            string instance,
            string traceId)
        {
            var pd = new ProblemDetails
            {
                Status = status,
                Title = title,
                Detail = detail,
                Type = "about:blank",
                Instance = instance
            };

            pd.Extensions["traceId"] = traceId;
            pd.Extensions["timestamp"] = DateTime.UtcNow;

            return pd;
        }
    }
}
