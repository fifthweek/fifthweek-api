namespace Fifthweek.Api.Posts.Controllers
{
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.CodeGeneration;

    [AutoConstructor, AutoEqualityMembers]
    public partial class RevisedNoteData
    {
        public RevisedNoteData()
        {
        }

        public ChannelId ChannelId { get; set; }

        [Parsed(typeof(ValidNote))]
        public string Note { get; set; }
    }
}