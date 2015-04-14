namespace Fifthweek.Api.Aggregations.Queries
{
    using System.Collections.Generic;

    using Fifthweek.Api.Blogs;
    using Fifthweek.Api.Blogs.Queries;
    using Fifthweek.Api.Collections.Queries;
    using Fifthweek.Api.FileManagement.Queries;
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;

    [AutoConstructor, AutoEqualityMembers]
    public partial class UserState
    {
        public UserAccessSignatures AccessSignatures { get; private set; }

        [Optional]
        public CreatorStatus CreatorStatus { get; private set; }
        
        [Optional]
        public ChannelsAndCollections CreatedChannelsAndCollections { get; private set; }

        [Optional]
        public GetAccountSettingsResult AccountSettings { get; private set; }

        [Optional]
        public BlogWithFileInformation Blog { get; private set; }

        [Optional]
        public GetUserSubscriptionsResult Subscriptions { get; private set; }
    }
}