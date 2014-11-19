namespace Fifthweek.Api.Queries
{
    using Fifthweek.Api.Entities;
    using Fifthweek.Api.Models;

    public class GetClientQuery : IQuery<Client>
    {
        public GetClientQuery(ClientId clientId)
        {
            this.ClientId = clientId;
        }

        public ClientId ClientId { get; private set; }
    }
}