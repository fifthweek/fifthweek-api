namespace Fifthweek.Api.Blogs.Controllers
{
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.CodeGeneration;

    [AutoEqualityMembers]
    public partial class UpdatedBlogData
    {
        [Parsed(typeof(ValidBlogName))]
        public string BlogName { get; set; }

        [Parsed(typeof(ValidTagline))]
        public string Tagline { get; set; }

        [Parsed(typeof(ValidIntroduction))]
        public string Introduction { get; set; }

        [Optional]
        public FileId HeaderImageFileId { get; set; }

        [Optional]
        [Parsed(typeof(ValidExternalVideoUrl))]
        public string Video { get; set; }

        [Parsed(typeof(ValidBlogDescription))]
        [Optional]
        public string Description { get; set; }
    }
}