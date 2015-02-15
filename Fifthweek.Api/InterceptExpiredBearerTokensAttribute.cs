namespace Fifthweek.Api
{
    using System.Net;
    using System.Net.Http;
    using System.ServiceModel.Channels;
    using System.Web;
    using System.Web.Http;
    using System.Web.Http.Controllers;
    using System.Web.Http.Filters;

    public class InterceptExpiredBearerTokensAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            // If they have supplied authentication information but are still not authenticated
            // then it is likely that their access token has expired so we return a 401
            // as soon as possible.  This also allows us not to log expired access tokens.
            var user = actionContext.RequestContext.Principal;
            var identity = user == null ? null : user.Identity;
            if (identity == null || identity.IsAuthenticated == false)
            {
                var authorizationHeader = actionContext.Request.Headers.Authorization;
                if (authorizationHeader != null)
                {
                    actionContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
                    return;
                }
            }

            base.OnActionExecuting(actionContext);
        }    
    }
}