namespace Fifthweek.Api.Subscriptions.Commands
{
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement;
    using Fifthweek.Api.Identity.Membership;

    [AutoEqualityMembers, AutoConstructor]
    public partial class UpdateSubscriptionCommand
    {
        public UserId Requester { get; private set; }

        public SubscriptionId SubscriptionId { get; private set; }

        public SubscriptionName SubscriptionName { get; private set; }

        public Tagline Tagline { get; private set; }

        public Introduction Introduction { get; private set; }

        [Optional]
        public Description Description { get; private set; }

        [Optional]
        public FileId HeaderImageFileId { get; private set; }

        [Optional]
        public ExternalVideoUrl Video { get; private set; }
    }
}