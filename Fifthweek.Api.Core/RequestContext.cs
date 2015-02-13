namespace Fifthweek.Api.Core
{
    using System.Web;

    public class RequestContext : IRequestContext
    {
        public RequestContext()
        {
            this.HttpContext = System.Web.HttpContext.Current;
        }

        public HttpContext HttpContext { get; private set; }
    }
}