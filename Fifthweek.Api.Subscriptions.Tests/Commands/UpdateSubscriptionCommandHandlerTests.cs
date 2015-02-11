namespace Fifthweek.Api.Subscriptions.Tests.Commands
{
    using System;
    using System.Data.Entity;
    using System.Net;
    using System.Threading.Tasks;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Identity.Tests.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Tests.Shared;
    using Fifthweek.Api.Subscriptions.Commands;
    using Fifthweek.Api.Subscriptions.Shared;
    using Fifthweek.Tests.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class UpdateSubscriptionCommandHandlerTests : PersistenceTestsBase
    {
        private static readonly UserId UserId = new UserId(Guid.NewGuid());
        private static readonly Requester Requester = Requester.Authenticated(UserId);
        private static readonly SubscriptionId SubscriptionId = new SubscriptionId(Guid.NewGuid());
        private static readonly ValidSubscriptionName SubscriptionName = ValidSubscriptionName.Parse("Lawrence");
        private static readonly ValidTagline Tagline = ValidTagline.Parse("Web Comics and More");
        private static readonly ValidIntroduction Introduction = ValidIntroduction.Default;
        private static readonly ValidSubscriptionDescription Description = ValidSubscriptionDescription.Parse("Hello all!");
        private static readonly FileId HeaderImageFileId = new FileId(Guid.NewGuid());
        private static readonly ValidExternalVideoUrl Video = ValidExternalVideoUrl.Parse("http://youtube.com/3135");
        private static readonly UpdateSubscriptionCommand Command = new UpdateSubscriptionCommand(
            Requester, 
            SubscriptionId, 
            SubscriptionName, 
            Tagline, 
            Introduction, 
            Description,
            HeaderImageFileId,
            Video);

        private Mock<ISubscriptionSecurity> subscriptionSecurity;
        private Mock<IFileSecurity> fileSecurity;
        private Mock<IRequesterSecurity> requesterSecurity;
        private UpdateSubscriptionCommandHandler target;

        [TestInitialize]
        public void Initialize()
        {
            this.subscriptionSecurity = new Mock<ISubscriptionSecurity>();
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

            this.target = new UpdateSubscriptionCommandHandler(this.subscriptionSecurity.Object, this.fileSecurity.Object, this.requesterSecurity.Object, connectionFactory.Object);
            
            await this.target.HandleAsync(new UpdateSubscriptionCommand(
                Requester.Unauthenticated,
                SubscriptionId,
                SubscriptionName,
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
                this.subscriptionSecurity.Setup(_ => _.AssertWriteAllowedAsync(UserId, SubscriptionId)).Throws<UnauthorizedException>();
                this.target = new UpdateSubscriptionCommandHandler(this.subscriptionSecurity.Object, this.fileSecurity.Object, this.requesterSecurity.Object, testDatabase);
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
                this.target = new UpdateSubscriptionCommandHandler(this.subscriptionSecurity.Object, this.fileSecurity.Object, this.requesterSecurity.Object, testDatabase);
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
                this.target = new UpdateSubscriptionCommandHandler(this.subscriptionSecurity.Object, this.fileSecurity.Object, this.requesterSecurity.Object, testDatabase);
                await this.CreateSubscriptionAsync(UserId, SubscriptionId, testDatabase);
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
                this.target = new UpdateSubscriptionCommandHandler(this.subscriptionSecurity.Object, this.fileSecurity.Object, this.requesterSecurity.Object, testDatabase);
                var subscription = await this.CreateSubscriptionAsync(UserId, SubscriptionId, testDatabase);
                await testDatabase.TakeSnapshotAsync();

                await this.target.HandleAsync(Command);

                var expectedSubscription = new Subscription(
                    SubscriptionId.Value,
                    UserId.Value,
                    null,
                    SubscriptionName.Value,
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

        private async Task<Subscription> CreateSubscriptionAsync(UserId newUserId, SubscriptionId newSubscriptionId, TestDatabaseContext testDatabase)
        {
            using (var databaseContext = testDatabase.CreateContext())
            {
                await databaseContext.CreateTestSubscriptionAsync(newUserId.Value, newSubscriptionId.Value, Guid.NewGuid());

                var newHeaderImage = FileTests.UniqueEntity(new Random());
                newHeaderImage.Id = HeaderImageFileId.Value;
                newHeaderImage.UserId = newUserId.Value;
                await databaseContext.Database.Connection.InsertAsync(newHeaderImage);
            }

            using (var databaseContext = testDatabase.CreateContext())
            {
                return await databaseContext.Subscriptions.SingleAsync(_ => _.Id == newSubscriptionId.Value);
            }
        }
    }
}