namespace Fifthweek.Api.Aggregations.Queries
{
    using System.Collections.Generic;

    using Fifthweek.Api.Collections.Queries;
    using Fifthweek.Api.FileManagement.Queries;
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.Api.Subscriptions;
    using Fifthweek.CodeGeneration;

    [AutoConstructor, AutoEqualityMembers]
    public partial class UserState
    {
        [Optional]
        public UserId UserId { get; private set; }

        public UserAccessSignatures AccessSignatures { get; private set; }

        [Optional]
        public CreatorStatus CreatorStatus { get; private set; }
        
        [Optional]
        public ChannelsAndCollections CreatedChannelsAndCollections { get; private set; }
    }
}