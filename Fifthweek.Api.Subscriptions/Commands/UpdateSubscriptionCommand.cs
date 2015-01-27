namespace Fifthweek.Api.Subscriptions.Commands
{
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Subscriptions.Shared;
    using Fifthweek.CodeGeneration;

    [AutoEqualityMembers, AutoConstructor]
    public partial class UpdateSubscriptionCommand
    {
        public Requester Requester { get; private set; }

        public SubscriptionId SubscriptionId { get; private set; }

        public ValidSubscriptionName SubscriptionName { get; private set; }

        public ValidTagline Tagline { get; private set; }

        public ValidIntroduction Introduction { get; private set; }

        [Optional]
        public ValidDescription Description { get; private set; }

        [Optional]
        public FileId HeaderImageFileId { get; private set; }

        [Optional]
        public ValidExternalVideoUrl Video { get; private set; }
    }
}