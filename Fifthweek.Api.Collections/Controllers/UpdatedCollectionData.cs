namespace Fifthweek.Api.Collections.Controllers
{
    using System.Collections.Generic;

    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.CodeGeneration;

    [AutoConstructor, AutoEqualityMembers]
    public partial class UpdatedCollectionData
    {
        public UpdatedCollectionData()
        {
        }

        public ChannelId ChannelId { get; set; }

        [Parsed(typeof(ValidCollectionName))]
        public string Name { get; set; }

        //[Parsed(typeof(WeeklyReleaseSchedule))]
        public List<byte> WeeklyReleaseTimes { get; set; }
    }
}