namespace Dexter.Api.Queries
{
    using Dexter.Api.Entities;
    using Dexter.Api.Models;

    public class GetClientQuery : IQuery<Client>
    {
        public GetClientQuery(ClientId clientId)
        {
            this.ClientId = clientId;
        }

        public ClientId ClientId { get; private set; }
    }
}