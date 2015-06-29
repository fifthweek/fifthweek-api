namespace Fifthweek.Api.Posts
{
    using System.Collections.Generic;

    using Fifthweek.Api.Posts.Queries;
    using Fifthweek.CodeGeneration;

    [AutoConstructor, AutoEqualityMembers]
    public partial class GetNewsfeedDbResult
    {
        public IReadOnlyList<NewsfeedPost> Posts { get; private set; }

        public int AccountBalance { get; private set; }
    }
}