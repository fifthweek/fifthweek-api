namespace Fifthweek.Api.Posts.Controllers
{
    using System;

    using Fifthweek.Api.Collections;
    using Fifthweek.Api.FileManagement;
    using Fifthweek.CodeGeneration;

    [AutoEqualityMembers]
    public partial class NewImageData
    {
        public CollectionId CollectionId { get; set; }

        public FileId ImageFileId { get; set; }
        
        [Optional]
        [Parsed(typeof(ValidComment))]
        public string Comment { get; set; }

        [Optional]
        public DateTime? ScheduledPostDate { get; set; }

        public bool IsQueued { get; set; }
    }
}