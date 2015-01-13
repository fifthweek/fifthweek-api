namespace Fifthweek.Api.Subscriptions.Controllers
{
    using System;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement;

    [AutoEqualityMembers]
    public partial class UpdatedSubscriptionData
    {
        [Parsed(typeof(ValidSubscriptionName))]
        public string SubscriptionName { get; set; }

        [Parsed(typeof(ValidTagline))]
        public string Tagline { get; set; }

        [Parsed(typeof(ValidIntroduction))]
        public string Introduction { get; set; }

        [Optional]
        [Constructed(typeof(FileId))]
        public Guid? HeaderImageFileId { get; set; }

        [Optional]
        [Parsed(typeof(ValidExternalVideoUrl))]
        public string Video { get; set; }

        [Parsed(typeof(ValidDescription))]
        public string Description { get; set; }
    }
}