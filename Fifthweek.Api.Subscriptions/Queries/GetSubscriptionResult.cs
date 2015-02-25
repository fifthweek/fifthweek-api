namespace Fifthweek.Api.Subscriptions.Queries
{
    using System;

    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Subscriptions.Shared;
    using Fifthweek.CodeGeneration;

    [AutoConstructor]
    [AutoEqualityMembers]
    public partial class GetSubscriptionResult
    {
        public SubscriptionId SubscriptionId { get; private set; }

        public UserId CreatorId { get; private set; }

        public SubscriptionName SubscriptionName { get; private set; }

        public Tagline Tagline { get; private set; }

        public Introduction Introduction { get; private set; }

        public DateTime CreationDate { get; private set; }

        [Optional]
        public FileInformation HeaderImage { get; private set; }

        [Optional]
        public ExternalVideoUrl Video { get; private set; }

        [Optional]
        public SubscriptionDescription Description { get; private set; }
    }
}