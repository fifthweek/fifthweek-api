﻿namespace Fifthweek.Api.Blogs.Tests.Controllers
{
    using System;
    using System.Threading.Tasks;
    using System.Web.Http.Results;

    using Fifthweek.Api.Blogs.Commands;
    using Fifthweek.Api.Blogs.Controllers;
    using Fifthweek.Api.Blogs.Queries;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class BlogControllerTests
    {
        private static readonly DateTime Now = DateTime.UtcNow;
        private static readonly UserId UserId = new UserId(Guid.NewGuid());
        private static readonly Requester Requester = Requester.Authenticated(UserId);
        private static readonly BlogId BlogId = new BlogId(Guid.NewGuid());
        private static readonly FileId HeaderImageFileId = new FileId(Guid.NewGuid());
        private Mock<ICommandHandler<CreateBlogCommand>> createBlog;
        private Mock<ICommandHandler<UpdateBlogCommand>> updateBlog;
        private Mock<IQueryHandler<GetBlogQuery, GetBlogResult>> getBlog;
        private Mock<IRequesterContext> requesterContext;
        private Mock<IGuidCreator> guidCreator;
        private BlogController target;

        [TestInitialize]
        public void Initialize()
        {
            this.createBlog = new Mock<ICommandHandler<CreateBlogCommand>>();
            this.updateBlog = new Mock<ICommandHandler<UpdateBlogCommand>>();
            this.getBlog = new Mock<IQueryHandler<GetBlogQuery, GetBlogResult>>();
            this.requesterContext = new Mock<IRequesterContext>();
            this.guidCreator = new Mock<IGuidCreator>();
            this.target = new BlogController(
                this.createBlog.Object,
                this.updateBlog.Object,
                this.getBlog.Object,
                this.requesterContext.Object,
                this.guidCreator.Object);
        }

        [TestMethod]
        public async Task WhenPostingBlog_ItShouldIssueCreateBlogCommand()
        {
            var data = NewCreateBlogData();
            var command = NewCreateBlogCommand(UserId, BlogId, data);

            this.requesterContext.Setup(v => v.GetRequester()).Returns(Requester);
            this.guidCreator.Setup(_ => _.CreateSqlSequential()).Returns(BlogId.Value);
            this.createBlog.Setup(v => v.HandleAsync(command)).Returns(Task.FromResult(0)).Verifiable();

            var result = await this.target.PostBlog(data);

            Assert.AreEqual(command.NewBlogId, result);
            this.createBlog.Verify();
        }

        [TestMethod]
        public async Task WhenPuttingBlog_ItShouldIssueUpdateBlogCommand()
        {
            var data = NewUpdatedBlogData();
            var command = NewUpdateBlogCommand(UserId, BlogId, data);

            this.requesterContext.Setup(v => v.GetRequester()).Returns(Requester);
            this.updateBlog.Setup(v => v.HandleAsync(command)).Returns(Task.FromResult(0)).Verifiable();

            var result = await this.target.PutBlog(BlogId.Value.EncodeGuid(), data);

            Assert.IsInstanceOfType(result, typeof(OkResult));
            this.updateBlog.Verify();
        }

        [TestMethod]
        public async Task WhenGettingBlog_ItShouldIssueGetBlogQuery()
        {
            this.getBlog.Setup(v => v.HandleAsync(new GetBlogQuery(BlogId)))
                .Returns(Task.FromResult(NewGetBlogResult())).Verifiable();

            var result = await this.target.GetBlog(BlogId.Value.EncodeGuid());

            Assert.AreEqual(NewGetBlogResult(), result);
            this.getBlog.Verify();
        }

        public static GetBlogResult NewGetBlogResult()
        {
            return new GetBlogResult(
                BlogId,
                UserId,
                new BlogName("name"),
                new Tagline("tagline"),
                new Introduction("intro"),
                Now,
                null,
                null,
                null);
        }

        public static NewBlogData NewCreateBlogData()
        {
            return new NewBlogData
            {
                BlogName = "Captain Phil",
                Tagline = "Web Comics And More",
                BasePrice = 50
            };
        }

        public static CreateBlogCommand NewCreateBlogCommand(
            UserId userId,
            BlogId blogId,
            NewBlogData data)
        {
            return new CreateBlogCommand(
                Requester.Authenticated(userId),
                blogId,
                ValidBlogName.Parse(data.BlogName),
                ValidTagline.Parse(data.Tagline),
                ValidChannelPriceInUsCentsPerWeek.Parse(data.BasePrice));
        }

        public static UpdatedBlogData NewUpdatedBlogData()
        {
            return new UpdatedBlogData
            {
                BlogName = "Captain Phil",
                Tagline = "Web Comics And More",
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
                ValidBlogName.Parse(data.BlogName),
                ValidTagline.Parse(data.Tagline),
                ValidIntroduction.Parse(data.Introduction),
                ValidBlogDescription.Parse(data.Description),
                data.HeaderImageFileId,
                ValidExternalVideoUrl.Parse(data.Video));
        }
    }
}