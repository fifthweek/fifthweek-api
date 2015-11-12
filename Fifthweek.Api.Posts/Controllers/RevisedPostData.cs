namespace Fifthweek.Api.Posts.Controllers
{
    using System.Collections.Generic;

    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.CodeGeneration;

    [AutoConstructor, AutoEqualityMembers]
    public partial class RevisedPostData
    {
        public RevisedPostData()
        {
        }

        [Optional]
        public FileId PreviewImageId { get; set; }

        [Optional]
        [Parsed(typeof(ValidPreviewText))]
        public string PreviewText { get; set; }

        [Optional]
        [Parsed(typeof(ValidComment))]
        public string Content { get; set; }

        public int PreviewWordCount { get; set; }
        
        public int WordCount { get; set; }

        public int ImageCount { get; set; }

        public int FileCount { get; set; }

        public int VideoCount { get; set; }

        [Optional]
        public List<FileId> FileIds { get; set; }
    }
}