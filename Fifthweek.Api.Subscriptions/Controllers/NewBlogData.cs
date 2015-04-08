namespace Fifthweek.Api.Subscriptions.Controllers
{
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Subscriptions.Shared;
    using Fifthweek.CodeGeneration;

    [AutoEqualityMembers]
    public partial class NewBlogData
    {
        [Parsed(typeof(ValidBlogName))]
        public string BlogName { get; set; }

        [Parsed(typeof(ValidTagline))]
        public string Tagline { get; set; }

        [Parsed(typeof(ValidChannelPriceInUsCentsPerWeek))]
        public int BasePrice { get; set; }
    }
}