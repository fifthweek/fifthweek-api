namespace Fifthweek.Api.Blogs.Controllers
{
    using System.Threading.Tasks;
    using System.Web.Http;

    using Fifthweek.Api.Blogs.Queries;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;

    [RoutePrefix("userBlogAccess"), AutoConstructor]
    public partial class UserBlogAccessController
    {
        private readonly IRequesterContext requesterContext;
        private readonly IQueryHandler<GetBlogAccessStatusQuery, GetBlogAccessStatusResult> getBlogAccessStatus;

        [Route("{blogId}")]
        public async Task<GetBlogAccessStatusResult> GetBlogAccessStatus(string blogId)
        {
            blogId.AssertUrlParameterProvided("blogId");

            var requester = this.requesterContext.GetRequester();
            var blogIdObject = new BlogId(blogId.DecodeGuid());
            return await this.getBlogAccessStatus.HandleAsync(new GetBlogAccessStatusQuery(requester, blogIdObject));
        }
    }
}