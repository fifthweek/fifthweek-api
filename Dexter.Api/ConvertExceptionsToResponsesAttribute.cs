namespace Dexter.Api
{
    using System.Net;
    using System.Net.Http;
    using System.Web.Http.Filters;

    public class ConvertExceptionsToResponsesAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            if (actionExecutedContext.Exception != null)
            {
                if (actionExecutedContext.Exception is BadRequestException)
                {
                    actionExecutedContext.Response = actionExecutedContext.Request.CreateErrorResponse(
                        HttpStatusCode.BadRequest, actionExecutedContext.Exception.Message);
                }
            }
        }
    }
}