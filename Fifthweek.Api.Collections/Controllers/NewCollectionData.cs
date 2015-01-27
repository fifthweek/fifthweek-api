namespace Fifthweek.Api.Collections.Controllers
{
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.CodeGeneration;

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