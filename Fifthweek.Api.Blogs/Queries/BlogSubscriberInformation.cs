﻿namespace Fifthweek.Api.Blogs.Queries
{
    using System;
    using System.Collections.Generic;

    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.CodeGeneration;

    [AutoConstructor, AutoEqualityMembers]
    public partial class BlogSubscriberInformation
    {
        public int UnreleasedRevenue { get; private set; }
      
        public int ReleasedRevenue { get; private set; }
      
        public int ReleasableRevenue { get; private set; }

        public IReadOnlyList<Subscriber> Subscribers { get; private set; }

        [AutoConstructor, AutoEqualityMembers]
        public partial class Subscriber
        {
            public Username Username { get; private set; }

            public UserId UserId { get; private set; }

            [Optional]
            public FileInformation ProfileImage { get; private set; }

            [Optional]
            public Email FreeAccessEmail { get; private set; }

            public PaymentStatus PaymentStatus { get; private set; }

            public bool HasPaymentInformation { get; private set; }

            public IReadOnlyList<SubscriberChannel> Channels { get; private set; }
        }

        [AutoConstructor, AutoEqualityMembers]
        public partial class SubscriberChannel
        {
            public ChannelId ChannelId { get; private set; }

            public DateTime SubscriptionStartDate { get; private set; }

            public int AcceptedPrice { get; private set; }
        }
    }
}