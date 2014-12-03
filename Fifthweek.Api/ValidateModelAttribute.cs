namespace Fifthweek.Api
{
    using System;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
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
                actionContext.Response = await RequestExceptionHandler.ReportExceptionAndCreateResponseAsync(actionContext.Request, exception);
            }
        }
    }
}