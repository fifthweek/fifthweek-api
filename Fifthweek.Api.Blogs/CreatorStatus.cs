namespace Fifthweek.Api.Blogs
{
    using Fifthweek.CodeGeneration;

    [AutoConstructor, AutoEqualityMembers]
    public partial class CreatorStatus
    {
        public static readonly CreatorStatus NoBlogs = new CreatorStatus(null, false);

        [Optional]
        public Shared.BlogId BlogId { get; private set; }

        public bool MustWriteFirstPost { get; private set; }
    }
}