namespace Fifthweek.Api
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Web.Http.Filters;

    using Fifthweek.Api.Services;

    public class ConvertExceptionsToResponsesAttribute : ActionFilterAttribute
    {
        public override async Task OnActionExecutedAsync(HttpActionExecutedContext actionExecutedContext, CancellationToken cancellationToken)
        {
            var exception = actionExecutedContext.Exception;
            if (exception != null)
            {
                actionExecutedContext.Response = await ExceptionHandlerUtilities.ReportExceptionAndCreateResponseAsync(actionExecutedContext.Request, exception);
            }
        }
    }
}