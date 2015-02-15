namespace Fifthweek.Api
{
    using System.Net.Http;
    using System.Web.Http.Filters;

    using Fifthweek.Api.Core;

    public class InitializeRequestContextAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            var requestContext = (IRequestContext)actionContext.Request.GetDependencyScope().GetService(typeof(IRequestContext));
            requestContext.Initialize(actionContext.Request, actionContext.RequestContext);
            base.OnActionExecuting(actionContext);
        }
    }
}