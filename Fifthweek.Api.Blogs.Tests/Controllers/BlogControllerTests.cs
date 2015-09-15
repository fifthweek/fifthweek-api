namespace Fifthweek.Api.Blogs.Tests.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Threading.Tasks;
    using System.Web.Http;
    using System.Web.Http.Results;

    using Fifthweek.Api.Blogs.Commands;
    using Fifthweek.Api.Blogs.Controllers;
    using Fifthweek.Api.Blogs.Queries;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Shared;
    using Fifthweek.Tests.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class BlogControllerTests
    {
        private static readonly DateTime Now = DateTime.UtcNow;
        private static readonly UserId UserId = new UserId(Guid.NewGuid());
        private static readonly Requester Requester = Requester.Authenticated(UserId);
        private static readonly BlogId BlogId = new BlogId(Guid.NewGuid());
        private static readonly ChannelId FirstChannelId = new ChannelId(Guid.NewGuid());
        private static readonly FileId HeaderImageFileId = new FileId(Guid.NewGuid());
        private static readonly Username Username = new Username("username");
        private Mock<ICommandHandler<CreateBlogCommand>> createBlog;
        private Mock<ICommandHandler<UpdateBlogCommand>> updateBlog;
        private Mock<IQueryHandler<GetLandingPageQuery, GetLandingPageResult>> getLandingPage;
        private Mock<IQueryHandler<GetBlogSubscriberInformationQuery, BlogSubscriberInformation>> getBlogSubscriberInformation;
        private Mock<IQueryHandler<GetAllCreatorRevenuesQuery, GetAllCreatorRevenuesResult>> getAllCreatorRevenues;
        private Mock<IRequesterContext> requesterContext;
        private Mock<IGuidCreator> guidCreator;
        private BlogController target;

        [TestInitialize]
        public void Initialize()
        {
            this.createBlog = new Mock<ICommandHandler<CreateBlogCommand>>(MockBehavior.Strict);
            this.updateBlog = new Mock<ICommandHandler<UpdateBlogCommand>>(MockBehavior.Strict);
            this.getLandingPage = new Mock<IQueryHandler<GetLandingPageQuery, GetLandingPageResult>>(MockBehavior.Strict);
            this.getBlogSubscriberInformation = new Mock<IQueryHandler<GetBlogSubscriberInformationQuery,BlogSubscriberInformation>>(MockBehavior.Strict);
            this.getAllCreatorRevenues = new Mock<IQueryHandler<GetAllCreatorRevenuesQuery,GetAllCreatorRevenuesResult>>(MockBehavior.Strict);
            this.requesterContext = new Mock<IRequesterContext>();
            this.guidCreator = new Mock<IGuidCreator>();
            this.target = new BlogController(
                this.createBlog.Object,
                this.updateBlog.Object,
                this.getLandingPage.Object,
                this.getBlogSubscriberInformation.Object,
                this.getAllCreatorRevenues.Object,
                this.requesterContext.Object,
                this.guidCreator.Object);
        }

        [TestMethod]
        public async Task WhenPostingBlog_ItShouldIssueCreateBlogCommand()
        {
            var data = NewCreateBlogData();
            var command = NewCreateBlogCommand(UserId, BlogId, FirstChannelId, data);

            this.requesterContext.Setup(_ => _.GetRequesterAsync()).ReturnsAsync(Requester);
            this.guidCreator.Setup(_ => _.CreateSqlSequential()).ReturnsInOrder(BlogId.Value, FirstChannelId.Value);
            this.createBlog.Setup(v => v.HandleAsync(command)).Returns(Task.FromResult(0)).Verifiable();

            var result = await this.target.PostBlog(data);

            Assert.AreEqual(new NewBlogResult(command.NewBlogId, command.FirstChannelId), result);
            this.createBlog.Verify();
        }

        [TestMethod]
        public async Task WhenPuttingBlog_ItShouldIssueUpdateBlogCommand()
        {
            var data = NewUpdatedBlogData();
            var command = NewUpdateBlogCommand(UserId, BlogId, data);

            this.requesterContext.Setup(_ => _.GetRequesterAsync()).ReturnsAsync(Requester);
            this.updateBlog.Setup(v => v.HandleAsync(command)).Returns(Task.FromResult(0)).Verifiable();

            var result = await this.target.PutBlog(BlogId.Value.EncodeGuid(), data);

            Assert.IsInstanceOfType(result, typeof(OkResult));
            this.updateBlog.Verify();
        }

        [TestMethod]
        public async Task WhenGettingLandingPage_ItShouldIssueGetLandingPageQuery()
        {
            this.getLandingPage.Setup(v => v.HandleAsync(new GetLandingPageQuery(Username)))
                .Returns(Task.FromResult(NewGetLandingPageResult())).Verifiable();

            var result = await this.target.GetLandingPage(Username.Value);

            Assert.AreEqual(NewGetLandingPageResult(), result);
            this.getLandingPage.Verify();
        }

        [TestMethod]
        public async Task WhenGettingLandingPage_AndLandingPageDoesNotExist_ItShouldThrowHttpResponseExceptionWith404()
        {
            this.getLandingPage.Setup(v => v.HandleAsync(new GetLandingPageQuery(Username)))
                .Returns(Task.FromResult<GetLandingPageResult>(null)).Verifiable();

            var exception = await ExpectedException.GetExceptionAsync<HttpResponseException>(() => this.target.GetLandingPage(Username.Value));

            Assert.AreEqual(HttpStatusCode.NotFound, exception.Response.StatusCode);
            this.getLandingPage.Verify();
        }

        [TestMethod]
        public async Task WhenGettingBlogSubscriberInformation_ItShouldIssueGetBlogSubscriberInformationQuery()
        {
            this.requesterContext.Setup(_ => _.GetRequesterAsync()).ReturnsAsync(Requester);

            var expectedResult = new BlogSubscriberInformation(10, 20, 30, new List<BlogSubscriberInformation.Subscriber>());
            this.getBlogSubscriberInformation.Setup(v => v.HandleAsync(new GetBlogSubscriberInformationQuery(Requester, BlogId)))
                .Returns(Task.FromResult(expectedResult)).Verifiable();

            var result = await this.target.GetSubscriberInformation(BlogId.Value.EncodeGuid());

            Assert.AreEqual(expectedResult, result);
            this.getBlogSubscriberInformation.Verify();
        }

        [TestMethod]
        public async Task WhenGettingCreatorRevenues_ItShouldIssueGetGetAllCreatorRevenuesQuery()
        {
            this.requesterContext.Setup(_ => _.GetRequesterAsync()).ReturnsAsync(Requester);

            var expectedResult = new GetAllCreatorRevenuesResult(new List<GetAllCreatorRevenuesResult.Creator>());
            this.getAllCreatorRevenues.Setup(v => v.HandleAsync(new GetAllCreatorRevenuesQuery(Requester)))
                .Returns(Task.FromResult(expectedResult)).Verifiable();

            var result = await this.target.GetCreatorRevenues();

            Assert.AreEqual(expectedResult, result);
            this.getAllCreatorRevenues.Verify();
        }

        public static GetLandingPageResult NewGetLandingPageResult()
        {
            return new GetLandingPageResult(
                UserId,
                null,
                new BlogWithFileInformation(
                    BlogId,
                    new BlogName("name"),
                    new Introduction("intro"),
                    Now,
                    null,
                    null,
                    null,
                    new List<ChannelResult>(),
                    new List<QueueResult>()));
        }

        public static NewBlogData NewCreateBlogData()
        {
            return new NewBlogData
            {
                Name = "Captain Phil",
                BasePrice = 50
            };
        }

        public static CreateBlogCommand NewCreateBlogCommand(
            UserId userId,
            BlogId blogId,
            ChannelId firstChannelId,
            NewBlogData data)
        {
            return new CreateBlogCommand(
                Requester.Authenticated(userId),
                blogId,
                firstChannelId,
                ValidBlogName.Parse(data.Name),
                ValidChannelPrice.Parse(data.BasePrice));
        }

        public static UpdatedBlogData NewUpdatedBlogData()
        {
            return new UpdatedBlogData
            {
                Name = "Captain Phil",
                Introduction = "Blog introduction",
                HeaderImageFileId = HeaderImageFileId,
                Video = "http://youtube.com/3135",
                Description = "Hello all!"
            };
        }

        public static UpdateBlogCommand NewUpdateBlogCommand(
            UserId userId,
            BlogId blogId,
            UpdatedBlogData data)
        {
            return new UpdateBlogCommand(
                Requester.Authenticated(userId),
                blogId,
                ValidBlogName.Parse(data.Name),
                ValidIntroduction.Parse(data.Introduction),
                ValidBlogDescription.Parse(data.Description),
                data.HeaderImageFileId,
                ValidExternalVideoUrl.Parse(data.Video));
        }
    }
}