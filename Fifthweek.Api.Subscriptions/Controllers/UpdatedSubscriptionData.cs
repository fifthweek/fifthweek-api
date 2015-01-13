namespace Fifthweek.Api.Subscriptions.Controllers
{
    using System;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement;

    [AutoEqualityMembers]
    public partial class UpdatedSubscriptionData
    {
        [Constructed(typeof(SubscriptionId))]
        public Guid SubscriptionId { get; set; }

        [Parsed(typeof(SubscriptionName))]
        public string SubscriptionName { get; set; }

        [Parsed(typeof(Tagline))]
        public string Tagline { get; set; }

        [Parsed(typeof(Introduction))]
        public string Introduction { get; set; }

        [Optional]
        [Constructed(typeof(FileId))]
        public Guid? HeaderImageFileId { get; set; }

        [Optional]
        [Parsed(typeof(ExternalVideoUrl))]
        public string Video { get; set; }

        [Parsed(typeof(Description))]
        public string Description { get; set; }
    }
}