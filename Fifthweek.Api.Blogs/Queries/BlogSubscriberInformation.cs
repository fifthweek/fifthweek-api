namespace Fifthweek.Api.Blogs.Queries
{
    using System;
    using System.Collections.Generic;

    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;

    [AutoConstructor, AutoEqualityMembers]
    public partial class BlogSubscriberInformation
    {
        public int TotalRevenue { get; private set; }

        public IReadOnlyList<Subscriber> Subscribers { get; private set; }

        [AutoConstructor, AutoEqualityMembers]
        public partial class Subscriber
        {
            public Username Username { get; set; }

            public UserId UserId { get; set; }

            [Optional]
            public FileInformation ProfileImage { get; set; }

            [Optional]
            public Email FreeAccessEmail { get; set; }

            public IReadOnlyList<SubscriberChannel> Channels { get; set; }
        }

        [AutoConstructor, AutoEqualityMembers]
        public partial class SubscriberChannel
        {
            public ChannelId ChannelId { get; set; }

            public DateTime SubscriptionStartDate { get; set; }

            public int AcceptedPrice { get; set; }
        }
    }
}