using CustomERP.Domain.Exceptions;
using System.Net;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace CustomERP.Api
{
    public class DomainExceptionFilterAttribute : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            switch (context.Exception)
            {
                case FormatException:
                case EntityNotFoundException:
                case BusinessRuleViolationException:
                case DuplicatedEntityException:
                    var statusCode = GetStatusCode(context.Exception);
                    context.Result = new ContentResult()
                    {
                        StatusCode = (int)statusCode,
                        Content = context.Exception.Message,
                    };
                    break;
                default:
                    return;
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
}