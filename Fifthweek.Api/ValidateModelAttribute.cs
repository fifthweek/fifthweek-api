namespace Fifthweek.Api
{
    using System.Threading;
    using System.Threading.Tasks;
    using System.Web;
    using System.Web.Http.Controllers;
    using System.Web.Http.Filters;

    using Fifthweek.Api.Core;

    public class ValidateModelAttribute : ActionFilterAttribute
    {
        public override async Task OnActionExecutingAsync(HttpActionContext actionContext, CancellationToken cancellationToken)
        {
            if (actionContext.ModelState.IsValid == false)
            {
                var exception = new ModelValidationException(actionContext.ModelState);
                var developerName = ExceptionHandlerUtilities.GetDeveloperName(HttpContext.Current);
                actionContext.Response = await ExceptionHandlerUtilities.ReportExceptionAndCreateResponseAsync(actionContext.Request, exception, developerName);
            }
        }
    }
}