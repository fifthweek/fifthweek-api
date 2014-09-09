namespace Dexter.Api.Queries
{
    public class GetUsernameFromExternalAccessTokenQuery : IQuery<string>
    {
        public GetUsernameFromExternalAccessTokenQuery(string provider, string externalAccessToken)
        {
            this.Provider = provider;
            this.ExternalAccessToken = externalAccessToken;
        }

        public string Provider { get; private set; }

        public string ExternalAccessToken { get; private set; }
    }
}