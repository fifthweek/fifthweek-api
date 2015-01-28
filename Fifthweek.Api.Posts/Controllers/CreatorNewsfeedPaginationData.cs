namespace Fifthweek.Api.Posts.Controllers
{
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;

    [AutoEqualityMembers]
    public partial class CreatorNewsfeedPaginationData
    {
        [Parsed(typeof(NonNegativeInt))]
        public int StartIndex { get; set; }

        [Parsed(typeof(PositiveInt))]
        public int Count { get; set; }
    }
}