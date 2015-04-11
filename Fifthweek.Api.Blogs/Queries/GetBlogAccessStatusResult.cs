namespace Fifthweek.Api.Blogs.Queries
{
    using Fifthweek.CodeGeneration;

    [AutoConstructor, AutoEqualityMembers]
    public partial class GetBlogAccessStatusResult
    {
        public bool GuestList { get; private set; }
    }
}