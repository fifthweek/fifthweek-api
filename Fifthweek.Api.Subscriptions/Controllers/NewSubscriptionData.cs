namespace Fifthweek.Api.Subscriptions.Controllers
{
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Subscriptions.Shared;
    using Fifthweek.CodeGeneration;

    [AutoEqualityMembers]
    public partial class NewSubscriptionData
    {
        [Parsed(typeof(ValidSubscriptionName))]
        public string SubscriptionName { get; set; }

        [Parsed(typeof(ValidTagline))]
        public string Tagline { get; set; }

        [Parsed(typeof(ValidChannelPriceInUsCentsPerWeek))]
        public int BasePrice { get; set; }
    }
}