namespace Fifthweek.Api.Posts
{
    using System.Collections.Generic;

    using Fifthweek.Api.Posts.Queries;
    using Fifthweek.CodeGeneration;

    [AutoConstructor, AutoEqualityMembers]
    public partial class GetPreviewNewsfeedDbResult
    {
        public IReadOnlyList<PreviewNewsfeedPost> Posts { get; private set; }
    }
}