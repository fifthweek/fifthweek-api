namespace Dexter.Api.Queries
{
    using Dexter.Api.Models;

    public class GetVerifiedAccessTokenQuery : IQuery<ParsedExternalAccessToken>
    {
        public GetVerifiedAccessTokenQuery(string provider, string accessToken)
        {
            this.Provider = provider;
            this.AccessToken = accessToken;
        }

        public string Provider { get; private set; }

        public string AccessToken { get; private set; }
    }
}