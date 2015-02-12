﻿namespace Fifthweek.Api
{
    using System.Net;
    using System.Net.Http;
    using System.Web;
    using System.Web.Http.Filters;

    public class InterceptExpiredBearerTokensAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            // If they have supplied authentication information but are still not authenticated
            // then it is likely that their access token has expired so we return a 401
            // as soon as possible.  This also allows us not to log expired access tokens.
            if (HttpContext.Current.User.Identity == null || HttpContext.Current.User.Identity.IsAuthenticated == false)
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