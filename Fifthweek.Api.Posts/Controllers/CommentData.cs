namespace Fifthweek.Api.Posts.Controllers
{
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.CodeGeneration;

    [AutoConstructor, AutoEqualityMembers]
    public partial class CommentData
    {
        public CommentData()
        {
        }

        [Parsed(typeof(ValidComment))]
        public string Content { get; set; }
    }
}