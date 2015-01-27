namespace Fifthweek.Api.Subscriptions.Controllers
{
    using Fifthweek.Api.FileManagement;
    using Fifthweek.CodeGeneration;

    using FileId = Fifthweek.Api.FileManagement.Shared.FileId;

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
        public FileId HeaderImageFileId { get; set; }

        [Optional]
        [Parsed(typeof(ValidExternalVideoUrl))]
        public string Video { get; set; }

        [Parsed(typeof(ValidDescription))]
        public string Description { get; set; }
    }
}