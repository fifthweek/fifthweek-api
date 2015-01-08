using System;
using Fifthweek.Api.Core;

namespace Fifthweek.Api.Subscriptions.Controllers
{
    [AutoEqualityMembers]
    public partial class MandatorySubscriptionData
    {
        [Constructed(typeof(SubscriptionId))]
        public Guid SubscriptionId { get; set; }

        [Parsed(typeof(SubscriptionName))]
        public string SubscriptionName { get; set; }

        [Parsed(typeof(Tagline))]
        public string Tagline { get; set; }

        [Parsed(typeof(ChannelPriceInUsCentsPerWeek))]
        public int BasePrice { get; set; }
    }
}