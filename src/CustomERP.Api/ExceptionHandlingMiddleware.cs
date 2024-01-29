using CustomERP.Domain.Exceptions;
using System.Net;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionHandlingMiddleware(RequestDelegate next)
    {
        _next = next ?? throw new ArgumentNullException(nameof(next));
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception exception)
        {
            switch (exception)
            {
                case FormatException:
                case EntityNotFoundException:
                case BusinessRuleViolationException:
                case DuplicatedEntityException:
                    var statusCode = GetStatusCode(exception);

                    context.Response.ContentType = "application/json";
                    context.Response.StatusCode = (int)statusCode;

                    await context.Response.WriteAsJsonAsync(exception.Message);
                    break;
                default:
                    throw;
            }
        }
    }

    private static HttpStatusCode GetStatusCode(Exception exception)
    {
        switch (exception)
        {
            case FormatException:
                return HttpStatusCode.BadRequest;
            case EntityNotFoundException:
                return HttpStatusCode.NotFound;
            case BusinessRuleViolationException:
                return HttpStatusCode.Forbidden;
            case DuplicatedEntityException:
                return HttpStatusCode.Conflict;
            default:
                return HttpStatusCode.InternalServerError;
        }
    }
}