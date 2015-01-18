﻿namespace Fifthweek.Api.Posts.Controllers
{
    using System;

    using Fifthweek.Api.Collections;
    using Fifthweek.Api.FileManagement;
    using Fifthweek.CodeGeneration;

    [AutoEqualityMembers]
    public partial class NewFileData
    {
        [Constructed(typeof(CollectionId), IsGuidBase64 = true)]
        public string CollectionId { get; set; }

        [Constructed(typeof(FileId), IsGuidBase64 = true)]
        public string FileId { get; set; }

        [Optional]
        [Parsed(typeof(ValidComment))]
        public string Comment { get; set; }

        [Optional]
        public DateTime? ScheduledPostDate { get; set; }

        public bool IsQueued { get; set; }
    }
}