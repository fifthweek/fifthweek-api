namespace Fifthweek.Api.Collections.Controllers
{
    using Fifthweek.Api.Channels;
    using Fifthweek.CodeGeneration;

    using ChannelId = Fifthweek.Api.Channels.Shared.ChannelId;

    [AutoConstructor, AutoEqualityMembers]
    public partial class NewCollectionData
    {
        public NewCollectionData()
        {
        }

        public ChannelId ChannelId { get; set; }

        [Parsed(typeof(ValidCollectionName))]
        public string Name { get; set; }
    }
}