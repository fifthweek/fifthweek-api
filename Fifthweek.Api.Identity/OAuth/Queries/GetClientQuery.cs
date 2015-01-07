namespace Fifthweek.Api.Identity.OAuth.Queries
{
    using Fifthweek.Api.Core;

    [AutoEqualityMembers]
    public partial class GetClientQuery : IQuery<Client>
    {
        public GetClientQuery(ClientId clientId)
        {
            this.ClientId = clientId;
        }

        public ClientId ClientId { get; private set; }
    }
}