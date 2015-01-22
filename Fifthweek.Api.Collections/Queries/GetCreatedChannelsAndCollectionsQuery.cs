namespace Fifthweek.Api.Collections.Queries
{
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.CodeGeneration;

    [AutoConstructor, AutoEqualityMembers]
    public partial class GetCreatedChannelsAndCollectionsQuery : IQuery<ChannelsAndCollections>
    {
        public Requester Requester { get; private set; }

        public UserId RequestedCreatorId { get; private set; }
    }
}