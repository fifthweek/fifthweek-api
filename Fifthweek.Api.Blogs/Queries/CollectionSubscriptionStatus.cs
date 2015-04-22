namespace Fifthweek.Api.Blogs.Queries
{
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.CodeGeneration;

    [AutoConstructor, AutoEqualityMembers]
    public partial class CollectionSubscriptionStatus
    {
        public CollectionId CollectionId { get; private set; }

        public string Name { get; private set; }
    }
}