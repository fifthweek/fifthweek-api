namespace Fifthweek.Api.Posts
{
    using System.Collections.Generic;

    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Posts.Queries;
    using Fifthweek.CodeGeneration;

    [AutoConstructor, AutoEqualityMembers]
    public partial class GetPostDbResult
    {
        public PreviewNewsfeedPost Post { get; private set; }

        public IReadOnlyList<PostFileDbResult> Files { get; private set; }

        [AutoConstructor, AutoEqualityMembers, AutoCopy]
        public partial class PostFileDbResult
        {
            public FileId FileId { get; private set; }

            public string FileName { get; private set; }

            public string FileExtension { get; private set; }

            public string Purpose { get; private set; }

            public long FileSize { get; private set; }

            [Optional]
            public int? RenderWidth { get; private set; }

            [Optional]
            public int? RenderHeight { get; private set; }
        }
    }
}