namespace Fifthweek.Api.Channels.Controllers
{
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.CodeGeneration;

    [AutoConstructor, AutoEqualityMembers]
    public partial class UpdatedChannelData
    {
        public UpdatedChannelData()
        {
        }

        [Parsed(typeof(ValidChannelName))]
        public string Name { get; set; }

        [Parsed(typeof(ValidChannelDescription))]
        public string Description { get; set; }

        [Parsed(typeof(ValidChannelPriceInUsCentsPerWeek))]
        public int Price { get; set; }

        public bool IsVisibleToNonSubscribers { get; set; }
    }
}