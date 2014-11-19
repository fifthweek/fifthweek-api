namespace Fifthweek.Api.Results
{
    using System.Net;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Web.Http;

    public class ChallengeResult : IHttpActionResult
    {
        private readonly string signInProvider;

        private readonly HttpRequestMessage request;

        public ChallengeResult(string signInProvider, HttpRequestMessage request)
        {
            this.signInProvider = signInProvider;
            this.request = request;
        }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            this.request.GetOwinContext().Authentication.Challenge(this.signInProvider);

            var response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
            response.RequestMessage = this.request;
            return Task.FromResult(response);
        }
    }
}