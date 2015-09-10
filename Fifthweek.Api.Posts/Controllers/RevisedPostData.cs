namespace Fifthweek.Api.Posts.Controllers
{
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.CodeGeneration;

    [AutoConstructor, AutoEqualityMembers]
    public partial class RevisedPostData
    {
        public RevisedPostData()
        {
        }

        [Optional]
        public FileId FileId { get; set; }

        [Optional]
        public FileId ImageId { get; set; }

        [Optional]
        [Parsed(typeof(ValidComment))]
        public string Comment { get; set; }
    }
}