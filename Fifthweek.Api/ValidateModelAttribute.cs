namespace Fifthweek.Api
{
    using System.Threading;
    using System.Threading.Tasks;
    using System.Web.Http.Controllers;
    using System.Web.Http.Filters;

    public class ValidateModelAttribute : ActionFilterAttribute
    {
        public override async Task OnActionExecutingAsync(HttpActionContext actionContext, CancellationToken cancellationToken)
        {
            if (actionContext.ModelState.IsValid == false)
            {
                var exception = new ModelValidationException(actionContext.ModelState);
                actionContext.Response = await ExceptionHandlerUtilities.ReportExceptionAndCreateResponseAsync(actionContext.Request, exception);
            }
        }
    }
}