namespace Fifthweek.Api.Posts.Controllers
{
    using System;

    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.CodeGeneration;

    [AutoConstructor, AutoEqualityMembers]
    public partial class NewImageData
    {
        public NewImageData()
        {
        }

        public CollectionId CollectionId { get; set; }

        public FileId FileId { get; set; }
        
        [Optional]
        [Parsed(typeof(ValidComment))]
        public string Comment { get; set; }

        [Optional]
        public DateTime? ScheduledPostTime { get; set; }

        public bool IsQueued { get; set; }
    }
}