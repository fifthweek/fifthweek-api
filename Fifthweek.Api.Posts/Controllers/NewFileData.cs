namespace Fifthweek.Api.Posts.Controllers
{
    using System;

    using Fifthweek.Api.Collections;
    using Fifthweek.Api.FileManagement;
    using Fifthweek.CodeGeneration;

    using CollectionId = Fifthweek.Api.Collections.Shared.CollectionId;
    using FileId = Fifthweek.Api.FileManagement.Shared.FileId;

    [AutoEqualityMembers]
    public partial class NewFileData
    {
        public CollectionId CollectionId { get; set; }

        public FileId FileId { get; set; }

        [Optional]
        [Parsed(typeof(ValidComment))]
        public string Comment { get; set; }

        [Optional]
        public DateTime? ScheduledPostDate { get; set; }

        public bool IsQueued { get; set; }
    }
}