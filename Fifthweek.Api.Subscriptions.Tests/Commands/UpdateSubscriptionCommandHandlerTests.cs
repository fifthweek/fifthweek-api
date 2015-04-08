namespace Fifthweek.Api.Blogs.Tests.Commands
{
    using System;
    using System.Data.Entity;
    using System.Threading.Tasks;

    using Fifthweek.Api.Blogs.Commands;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Identity.Tests.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Tests.Shared;
    using Fifthweek.Tests.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class UpdateSubscriptionCommandHandlerTests : PersistenceTestsBase
    {
        private static readonly UserId UserId = new UserId(Guid.NewGuid());
        private static readonly Requester Requester = Requester.Authenticated(UserId);
        private static readonly BlogId BlogId = new BlogId(Guid.NewGuid());
        private static readonly ValidBlogName BlogName = ValidBlogName.Parse("Lawrence");
        private static readonly ValidTagline Tagline = ValidTagline.Parse("Web Comics and More");
        private static readonly ValidIntroduction Introduction = ValidIntroduction.Default;
        private static readonly ValidBlogDescription Description = ValidBlogDescription.Parse("Hello all!");
        private static readonly FileId HeaderImageFileId = new FileId(Guid.NewGuid());
        private static readonly ValidExternalVideoUrl Video = ValidExternalVideoUrl.Parse("http://youtube.com/3135");
        private static readonly UpdateBlogCommand Command = new UpdateBlogCommand(
            Requester,
            BlogId,
            BlogName,
            Tagline,
            Introduction,
            Description,
            HeaderImageFileId,
            Video);
        private static readonly UpdateBlogCommand CommandWithoutHeaderImage = new UpdateBlogCommand(
            Requester,
            BlogId,
            BlogName,
            Tagline,
            Introduction,
            Description,
            null,
            Video);

        private Mock<IBlogSecurity> subscriptionSecurity;
        private Mock<IFileSecurity> fileSecurity;
        private Mock<IRequesterSecurity> requesterSecurity;
        private UpdateBlogCommandHandler target;

        [TestInitialize]
        public void Initialize()
        {
            this.subscriptionSecurity = new Mock<IBlogSecurity>();
            this.fileSecurity = new Mock<IFileSecurity>();
            this.requesterSecurity = new Mock<IRequesterSecurity>();
            this.requesterSecurity.SetupFor(Requester);
        }

        [TestMethod]
        [ExpectedException(typeof(UnauthorizedException))]
        public async Task WhenUnauthenticated_ItShouldThrowUnauthorizedException()
        {
            // Give side-effecting components strict mock behaviour.
            var connectionFactory = new Mock<IFifthweekDbConnectionFactory>(MockBehavior.Strict);

            this.target = new UpdateBlogCommandHandler(this.subscriptionSecurity.Object, this.fileSecurity.Object, this.requesterSecurity.Object, connectionFactory.Object);
            
            await this.target.HandleAsync(new UpdateBlogCommand(
                Requester.Unauthenticated,
                BlogId,
                BlogName,
                Tagline,
                Introduction,
                Description,
                HeaderImageFileId,
                Video));
        }

        [TestMethod]
        public async Task WhenNotAllowedToUpdate_ItShouldReportAnError()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.subscriptionSecurity.Setup(_ => _.AssertWriteAllowedAsync(UserId, BlogId)).Throws<UnauthorizedException>();
                this.target = new UpdateBlogCommandHandler(this.subscriptionSecurity.Object, this.fileSecurity.Object, this.requesterSecurity.Object, testDatabase);
                await testDatabase.TakeSnapshotAsync();

                Func<Task> badMethodCall = () => this.target.HandleAsync(Command);

                await badMethodCall.AssertExceptionAsync<UnauthorizedException>();

                return ExpectedSideEffects.None;
            });
        }

        [TestMethod]
        public async Task WhenNotAllowedToUseFile_ItShouldReportAnError()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.fileSecurity.Setup(_ => _.AssertReferenceAllowedAsync(UserId, HeaderImageFileId)).Throws<UnauthorizedException>();
                this.target = new UpdateBlogCommandHandler(this.subscriptionSecurity.Object, this.fileSecurity.Object, this.requesterSecurity.Object, testDatabase);
                await testDatabase.TakeSnapshotAsync();

                Func<Task> badMethodCall = () => this.target.HandleAsync(Command);

                await badMethodCall.AssertExceptionAsync<UnauthorizedException>();

                return ExpectedSideEffects.None;
            });
        }

        [TestMethod]
        public async Task WhenReRun_ItShouldHaveNoEffect()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new UpdateBlogCommandHandler(this.subscriptionSecurity.Object, this.fileSecurity.Object, this.requesterSecurity.Object, testDatabase);
                await this.CreateSubscriptionAsync(UserId, BlogId, testDatabase);
                await this.target.HandleAsync(Command);
                await testDatabase.TakeSnapshotAsync();

                await this.target.HandleAsync(Command);

                return ExpectedSideEffects.None;
            });
        }

        [TestMethod]
        public async Task ItShouldUpdateSubscription()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new UpdateBlogCommandHandler(this.subscriptionSecurity.Object, this.fileSecurity.Object, this.requesterSecurity.Object, testDatabase);
                var subscription = await this.CreateSubscriptionAsync(UserId, BlogId, testDatabase);
                await testDatabase.TakeSnapshotAsync();

                await this.target.HandleAsync(Command);

                var expectedSubscription = new Blog(
                    BlogId.Value,
                    UserId.Value,
                    null,
                    BlogName.Value,
                    Tagline.Value,
                    Introduction.Value,
                    Description.Value,
                    Video.Value,
                    HeaderImageFileId.Value,
                    null,
                    subscription.CreationDate);

                return new ExpectedSideEffects
                {
                    Update = expectedSubscription
                };
            });
        }

        [TestMethod]
        public async Task WhenNoHeaderImage_ItShouldUpdateSubscription()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new UpdateBlogCommandHandler(this.subscriptionSecurity.Object, this.fileSecurity.Object, this.requesterSecurity.Object, testDatabase);
                var subscription = await this.CreateSubscriptionAsync(UserId, BlogId, testDatabase);
                await testDatabase.TakeSnapshotAsync();

                await this.target.HandleAsync(CommandWithoutHeaderImage);

                var expectedSubscription = new Blog(
                    BlogId.Value,
                    UserId.Value,
                    null,
                    BlogName.Value,
                    Tagline.Value,
                    Introduction.Value,
                    Description.Value,
                    Video.Value,
                    null,
                    null,
                    subscription.CreationDate);

                return new ExpectedSideEffects
                {
                    Update = expectedSubscription
                };
            });
        }

        private async Task<Blog> CreateSubscriptionAsync(UserId newUserId, BlogId newBlogId, TestDatabaseContext testDatabase)
        {
            using (var databaseContext = testDatabase.CreateContext())
            {
                await databaseContext.CreateTestSubscriptionAsync(newUserId.Value, newBlogId.Value, Guid.NewGuid());

                var newHeaderImage = FileTests.UniqueEntity(new Random());
                newHeaderImage.Id = HeaderImageFileId.Value;
                newHeaderImage.UserId = newUserId.Value;
                await databaseContext.Database.Connection.InsertAsync(newHeaderImage);
            }

            using (var databaseContext = testDatabase.CreateContext())
            {
                return await databaseContext.Blogs.SingleAsync(_ => _.Id == newBlogId.Value);
            }
        }
    }
}