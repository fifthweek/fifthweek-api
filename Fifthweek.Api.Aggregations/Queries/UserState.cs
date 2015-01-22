namespace Fifthweek.Api.Aggregations.Queries
{
    using Fifthweek.Api.Collections.Queries;
    using Fifthweek.Api.Subscriptions;
    using Fifthweek.CodeGeneration;

    [AutoConstructor, AutoEqualityMembers]
    public partial class UserState
    {
        [Optional]
        public CreatorStatus CreatorStatus { get; private set; }
        
        [Optional]
        public ChannelsAndCollections CreatedChannelsAndCollections { get; private set; }
    }
}