namespace Fifthweek.Api.Posts.Controllers
{
    using System;

    using Fifthweek.Api.FileManagement;
    using Fifthweek.Api.Subscriptions;
    using Fifthweek.CodeGeneration;

    [AutoEqualityMembers]
    public partial class NewFileData
    {
        [Optional]
        [Constructed(typeof(CollectionId), IsGuidBase64 = true)]
        public string CollectionId { get; set; }

        [Constructed(typeof(FileId), IsGuidBase64 = true)]
        public string FileId { get; set; }

        [Optional]
        public DateTime? ScheduledPostDate { get; set; }

        public bool IsQueued { get; set; }
    }
}