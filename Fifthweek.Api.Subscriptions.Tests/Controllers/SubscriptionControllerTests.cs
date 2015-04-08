namespace Fifthweek.Api.Subscriptions.Tests.Controllers
{
    using System;
    using System.Threading.Tasks;
    using System.Web.Http.Results;

    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Subscriptions.Commands;
    using Fifthweek.Api.Subscriptions.Controllers;
    using Fifthweek.Api.Subscriptions.Queries;
    using Fifthweek.Api.Subscriptions.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class SubscriptionControllerTests
    {
        private static readonly DateTime Now = DateTime.UtcNow;
        private static readonly UserId UserId = new UserId(Guid.NewGuid());
        private static readonly Requester Requester = Requester.Authenticated(UserId);
        private static readonly BlogId BlogId = new BlogId(Guid.NewGuid());
        private static readonly FileId HeaderImageFileId = new FileId(Guid.NewGuid());
        private Mock<ICommandHandler<CreateBlogCommand>> createSubscription;
        private Mock<ICommandHandler<UpdateBlogCommand>> updateSubscription;
        private Mock<IQueryHandler<GetBlogQuery, GetBlogResult>> getSubscription;
        private Mock<IRequesterContext> requesterContext;
        private Mock<IGuidCreator> guidCreator;
        private BlogController target;

        [TestInitialize]
        public void Initialize()
        {
            this.createSubscription = new Mock<ICommandHandler<CreateBlogCommand>>();
            this.updateSubscription = new Mock<ICommandHandler<UpdateBlogCommand>>();
            this.getSubscription = new Mock<IQueryHandler<GetBlogQuery, GetBlogResult>>();
            this.requesterContext = new Mock<IRequesterContext>();
            this.guidCreator = new Mock<IGuidCreator>();
            this.target = new BlogController(
                this.createSubscription.Object,
                this.updateSubscription.Object,
                this.getSubscription.Object,
                this.requesterContext.Object,
                this.guidCreator.Object);
        }

        [TestMethod]
        public async Task WhenPostingSubscription_ItShouldIssueCreateSubscriptionCommand()
        {
            var data = NewCreateSubscriptionData();
            var command = NewCreateSubscriptionCommand(UserId, BlogId, data);

            this.requesterContext.Setup(v => v.GetRequester()).Returns(Requester);
            this.guidCreator.Setup(_ => _.CreateSqlSequential()).Returns(BlogId.Value);
            this.createSubscription.Setup(v => v.HandleAsync(command)).Returns(Task.FromResult(0)).Verifiable();

            var result = await this.target.PostSubscription(data);

            Assert.AreEqual(command.NewBlogId, result);
            this.createSubscription.Verify();
        }

        [TestMethod]
        public async Task WhenPuttingSubscription_ItShouldIssueUpdateSubscriptionCommand()
        {
            var data = NewUpdatedSubscriptionData();
            var command = NewUpdateSubscriptionCommand(UserId, BlogId, data);

            this.requesterContext.Setup(v => v.GetRequester()).Returns(Requester);
            this.updateSubscription.Setup(v => v.HandleAsync(command)).Returns(Task.FromResult(0)).Verifiable();

            var result = await this.target.PutSubscription(BlogId.Value.EncodeGuid(), data);

            Assert.IsInstanceOfType(result, typeof(OkResult));
            this.updateSubscription.Verify();
        }

        [TestMethod]
        public async Task WhenGettingSubscription_ItShouldIssueGetSubscriptionQuery()
        {
            this.getSubscription.Setup(v => v.HandleAsync(new GetBlogQuery(BlogId)))
                .Returns(Task.FromResult(NewGetSubscriptionResult())).Verifiable();

            var result = await this.target.GetSubscription(BlogId.Value.EncodeGuid());

            Assert.AreEqual(NewGetSubscriptionResult(), result);
            this.getSubscription.Verify();
        }

        public static GetBlogResult NewGetSubscriptionResult()
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

        public static NewBlogData NewCreateSubscriptionData()
        {
            return new NewBlogData
            {
                BlogName = "Captain Phil",
                Tagline = "Web Comics And More",
                BasePrice = 50
            };
        }

        public static CreateBlogCommand NewCreateSubscriptionCommand(
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

        public static UpdatedBlogData NewUpdatedSubscriptionData()
        {
            return new UpdatedBlogData
            {
                SubscriptionName = "Captain Phil",
                Tagline = "Web Comics And More",
                Introduction = "Subscription introduction",
                HeaderImageFileId = HeaderImageFileId,
                Video = "http://youtube.com/3135",
                Description = "Hello all!"
            };
        }

        public static UpdateBlogCommand NewUpdateSubscriptionCommand(
            UserId userId,
            BlogId blogId,
            UpdatedBlogData data)
        {
            return new UpdateBlogCommand(
                Requester.Authenticated(userId),
                blogId,
                ValidBlogName.Parse(data.SubscriptionName),
                ValidTagline.Parse(data.Tagline),
                ValidIntroduction.Parse(data.Introduction),
                ValidBlogDescription.Parse(data.Description),
                data.HeaderImageFileId,
                ValidExternalVideoUrl.Parse(data.Video));
        }
    }
}