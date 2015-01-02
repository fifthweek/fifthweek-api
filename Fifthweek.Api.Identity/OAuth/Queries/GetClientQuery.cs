namespace Fifthweek.Api.Identity.OAuth.Queries
{
    using Fifthweek.Api.Core;

    public class GetClientQuery : IQuery<Client>
    {
        public GetClientQuery(ClientId clientId)
        {
            this.ClientId = clientId;
        }

        public ClientId ClientId { get; private set; }
    }
}