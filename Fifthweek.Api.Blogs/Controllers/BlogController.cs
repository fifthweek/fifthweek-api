namespace Fifthweek.Api.Blogs.Controllers
{
    using System.Net;
    using System.Threading.Tasks;
    using System.Web.Http;

    using Fifthweek.Api.Blogs.Commands;
    using Fifthweek.Api.Blogs.Queries;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;

    [RoutePrefix("blogs"), AutoConstructor]
    public partial class BlogController : ApiController
    {
        private readonly ICommandHandler<CreateBlogCommand> createBlog;
        private readonly ICommandHandler<UpdateBlogCommand> updateBlog;
        private readonly IQueryHandler<GetLandingPageQuery, GetLandingPageResult> getLandingPage;
        private readonly IQueryHandler<GetBlogSubscriberInformationQuery, BlogSubscriberInformation> getBlogSubscriberInformation;
        private readonly IQueryHandler<GetAllCreatorRevenuesQuery, GetAllCreatorRevenuesResult> getAllCreatorRevenues;
        private readonly IRequesterContext requesterContext;
        private readonly IGuidCreator guidCreator;

        [Route("")]
        public async Task<BlogId> PostBlog(NewBlogData blogData)
        {
            blogData.AssertBodyProvided("blogData");
            var blog = blogData.Parse();

            var requester = await this.requesterContext.GetRequesterAsync();
            var newBlogId = new BlogId(this.guidCreator.CreateSqlSequential());

            await this.createBlog.HandleAsync(new CreateBlogCommand(
                requester,
                newBlogId,
                blog.Name,
                blog.Tagline,
                blog.BasePrice));

            return newBlogId;
        }

        [Route("{blogId}")]
        public async Task<IHttpActionResult> PutBlog(string blogId, [FromBody]UpdatedBlogData blogData)
        {
            blogId.AssertUrlParameterProvided("blogId");
            blogData.AssertBodyProvided("blogData");
            var blog = blogData.Parse();

            var requester = await this.requesterContext.GetRequesterAsync();
            var blogIdObject = new BlogId(blogId.DecodeGuid());

            await this.updateBlog.HandleAsync(new UpdateBlogCommand(
                requester,
                blogIdObject,
                blog.Name,
                blog.Tagline,
                blog.Introduction,
                blog.Description,
                blog.HeaderImageFileId,
                blog.Video));

            return this.Ok();
        }

        [Route("landingPages/{username}")]
        public async Task<GetLandingPageResult> GetLandingPage(string username)
        {
            username.AssertUrlParameterProvided("username");

            var result = await this.getLandingPage.HandleAsync(new GetLandingPageQuery(new Username(username)));

            if (result == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            return result;
        }

        [Route("subscribers/{blogId}")]
        public async Task<BlogSubscriberInformation> GetSubscriberInformation(string blogId)
        {
            blogId.AssertUrlParameterProvided("blogId");

            var requester = await this.requesterContext.GetRequesterAsync();
            var blogIdObject = new BlogId(blogId.DecodeGuid());

            return await this.getBlogSubscriberInformation.HandleAsync(new GetBlogSubscriberInformationQuery(requester, blogIdObject));
        }

        [Route("creatorRevenues")]
        public async Task<GetAllCreatorRevenuesResult> GetCreatorRevenues()
        {
            var requester = await this.requesterContext.GetRequesterAsync();

            return await this.getAllCreatorRevenues.HandleAsync(new GetAllCreatorRevenuesQuery(requester));
        }
    }
}
