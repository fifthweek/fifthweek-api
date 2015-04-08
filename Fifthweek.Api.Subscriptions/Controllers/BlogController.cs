namespace Fifthweek.Api.Blogs.Controllers
{
    using System.Threading.Tasks;
    using System.Web.Http;

    using Fifthweek.Api.Blogs.Commands;
    using Fifthweek.Api.Blogs.Queries;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;

    [RoutePrefix("blogs"), AutoConstructor]
    public partial class BlogController : ApiController
    {
        private readonly ICommandHandler<CreateBlogCommand> createBlog;
        private readonly ICommandHandler<UpdateBlogCommand> updateBlog;
        private readonly IQueryHandler<GetBlogQuery, GetBlogResult> getBlog;
        private readonly IRequesterContext requesterContext;
        private readonly IGuidCreator guidCreator;

        [Route("")]
        public async Task<BlogId> PostSubscription(NewBlogData blogData)
        {
            blogData.AssertBodyProvided("blogData");
            var blog = blogData.Parse();

            var requester = this.requesterContext.GetRequester();
            var newBlogId = new BlogId(this.guidCreator.CreateSqlSequential());

            await this.createBlog.HandleAsync(new CreateBlogCommand(
                requester,
                newBlogId,
                blog.BlogName,
                blog.Tagline,
                blog.BasePrice));

            return newBlogId;
        }

        [Route("{blogId}")]
        public async Task<IHttpActionResult> PutSubscription(string blogId, [FromBody]UpdatedBlogData blogData)
        {
            blogId.AssertUrlParameterProvided("blogId");
            blogData.AssertBodyProvided("blogData");
            var blog = blogData.Parse();

            var requester = this.requesterContext.GetRequester();
            var blogIdObject = new BlogId(blogId.DecodeGuid());

            await this.updateBlog.HandleAsync(new UpdateBlogCommand(
                requester,
                blogIdObject,
                blog.BlogName,
                blog.Tagline,
                blog.Introduction,
                blog.Description,
                blog.HeaderImageFileId,
                blog.Video));

            return this.Ok();
        }

        [Route("{blogId}")]
        public async Task<GetBlogResult> GetSubscription(string blogId)
        {
            blogId.AssertUrlParameterProvided("blogId");

            var blogIdObject = new BlogId(blogId.DecodeGuid());

            var result = await this.getBlog.HandleAsync(new GetBlogQuery(
                blogIdObject));

            return result;
        }
    }
}
