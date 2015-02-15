namespace Fifthweek.Api.Core
{
    using System.Net.Http;
    using System.Web;
    using System.Web.Http.Controllers;

    public interface IRequestContext
    {
        HttpRequestMessage Request { get; }

        HttpRequestContext Context { get; }

        void Initialize(HttpRequestMessage request, HttpRequestContext context);
    }
}