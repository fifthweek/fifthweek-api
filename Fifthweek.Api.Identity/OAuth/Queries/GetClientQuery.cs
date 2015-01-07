namespace Fifthweek.Api.Identity.OAuth.Queries
{
    using Fifthweek.Api.Core;

    [AutoConstructor]
    [AutoEqualityMembers]
    public partial class GetClientQuery : IQuery<Client>
    {
        public ClientId ClientId { get; private set; }
    }
}