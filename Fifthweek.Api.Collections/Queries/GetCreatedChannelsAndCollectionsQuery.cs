namespace Fifthweek.Api.Collections.Queries
{
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;

    using UserId = Fifthweek.Api.Identity.Shared.Membership.UserId;

    [AutoConstructor, AutoEqualityMembers]
    public partial class GetCreatedChannelsAndCollectionsQuery : IQuery<ChannelsAndCollections>
    {
        public Requester Requester { get; private set; }

        public UserId RequestedCreatorId { get; private set; }
    }
}