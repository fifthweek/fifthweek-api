namespace Fifthweek.Api
{
    using System.Threading;
    using System.Threading.Tasks;
    using System.Web;
    using System.Web.Http.Filters;

    using Fifthweek.Api.Core;

    public class ConvertExceptionsToResponsesAttribute : ActionFilterAttribute
    {
        private HttpContext requestHttpContext;
        
        public override void OnActionExecuting(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            this.requestHttpContext = HttpContext.Current;
            base.OnActionExecuting(actionContext);
        }

        public override async Task OnActionExecutedAsync(HttpActionExecutedContext actionExecutedContext, CancellationToken cancellationToken)
        {
            var exception = actionExecutedContext.Exception;
            if (exception != null)
            {
                var developerName = ExceptionHandlerUtilities.GetDeveloperName(this.requestHttpContext);
                actionExecutedContext.Response = await ExceptionHandlerUtilities.ReportExceptionAndCreateResponseAsync(actionExecutedContext.Request, exception, developerName);
            }
        }
    }
}