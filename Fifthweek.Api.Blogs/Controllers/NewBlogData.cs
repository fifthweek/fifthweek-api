namespace Fifthweek.Api.Blogs.Controllers
{
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.CodeGeneration;

    [AutoEqualityMembers]
    public partial class NewBlogData
    {
        [Parsed(typeof(ValidBlogName))]
        public string Name { get; set; }

        [Parsed(typeof(ValidTagline))]
        public string Tagline { get; set; }

        [Parsed(typeof(ValidChannelPriceInUsCentsPerWeek))]
        public int BasePrice { get; set; }
    }
}