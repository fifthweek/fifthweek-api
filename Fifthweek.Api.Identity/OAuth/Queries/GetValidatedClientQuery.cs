namespace Fifthweek.Api.Identity.OAuth.Queries
{
    using Fifthweek.Api.Core;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;

    [AutoEqualityMembers, AutoConstructor]
    public partial class GetValidatedClientQuery : IQuery<Client>
    {
        public ClientId ClientId { get; private set; }

        [Optional]
        public string ClientSecret { get; private set; }
    }
}