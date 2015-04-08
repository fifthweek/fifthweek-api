namespace Fifthweek.Api.Subscriptions
{
    using Fifthweek.CodeGeneration;

    [AutoConstructor, AutoEqualityMembers]
    public partial class CreatorStatus
    {
        public static readonly CreatorStatus NoSubscriptions = new CreatorStatus(null, false);

        [Optional]
        public Shared.BlogId BlogId { get; private set; }

        public bool MustWriteFirstPost { get; private set; }
    }
}