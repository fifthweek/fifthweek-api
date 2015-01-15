namespace Fifthweek.Api.Subscriptions.Tests.Commands
{
    using System;
    using System.Data.Entity;
    using System.Threading.Tasks;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement;
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Tests.Shared;
    using Fifthweek.Api.Subscriptions.Commands;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class UpdateSubscriptionCommandHandlerTests : PersistenceTestsBase
    {
        private static readonly UserId UserId = new UserId(Guid.NewGuid());
        private static readonly SubscriptionId SubscriptionId = new SubscriptionId(Guid.NewGuid());
        private static readonly ValidSubscriptionName SubscriptionName = ValidSubscriptionName.Parse("Lawrence");
        private static readonly ValidTagline Tagline = ValidTagline.Parse("Web Comics and More");
        private static readonly ValidIntroduction Introduction = ValidIntroduction.Default;
        private static readonly ValidDescription Description = ValidDescription.Parse("Hello all!");
        private static readonly FileId HeaderImageFileId = new FileId(Guid.NewGuid());
        private static readonly ValidExternalVideoUrl Video = ValidExternalVideoUrl.Parse("http://youtube.com/3135");
        private static readonly UpdateSubscriptionCommand Command = new UpdateSubscriptionCommand(
            UserId, 
            SubscriptionId, 
            SubscriptionName, 
            Tagline, 
            Introduction, 
            Description,
            HeaderImageFileId,
            Video);

        private Mock<ISubscriptionSecurity> subscriptionSecurity;
        private Mock<IFileSecurity> fileSecurity;
        private UpdateSubscriptionCommandHandler target;

        [TestInitialize]
        public void Initialize()
        {
            this.subscriptionSecurity = new Mock<ISubscriptionSecurity>();
            this.fileSecurity = new Mock<IFileSecurity>();
        }

        [TestMethod]
        public async Task WhenNotAllowedToUpdate_ItShouldReportAnError()
        {
            await this.NewTestDatabaseAsync(async testDatabase =>
            {
                this.subscriptionSecurity.Setup(_ => _.AssertUpdateAllowedAsync(UserId, SubscriptionId)).Throws<UnauthorizedException>();
                this.target = new UpdateSubscriptionCommandHandler(this.subscriptionSecurity.Object, this.fileSecurity.Object, testDatabase.NewContext());
                await testDatabase.TakeSnapshotAsync();

                try
                {
                    await this.target.HandleAsync(Command);
                    Assert.Fail("Expected unauthorized exception");
                }
                catch (UnauthorizedException)
                {
                }

                return ExpectedSideEffects.None;
            });
        }

        [TestMethod]
        public async Task WhenNotAllowedToUseFile_ItShouldReportAnError()
        {
            await this.NewTestDatabaseAsync(async testDatabase =>
            {
                this.fileSecurity.Setup(_ => _.AssertFileBelongsToUserAsync(UserId, HeaderImageFileId)).Throws<UnauthorizedException>();
                this.target = new UpdateSubscriptionCommandHandler(this.subscriptionSecurity.Object, this.fileSecurity.Object, testDatabase.NewContext());
                await testDatabase.TakeSnapshotAsync();

                try
                {
                    await this.target.HandleAsync(Command);
                    Assert.Fail("Expected unauthorized exception");
                }
                catch (UnauthorizedException)
                {
                }

                return ExpectedSideEffects.None;
            });
        }

        [TestMethod]
        public async Task WhenReRun_ItShouldHaveNoEffect()
        {
            await this.NewTestDatabaseAsync(async testDatabase =>
            {
                this.target = new UpdateSubscriptionCommandHandler(this.subscriptionSecurity.Object, this.fileSecurity.Object, testDatabase.NewContext());
                await this.CreateSubscriptionAsync(UserId, SubscriptionId, HeaderImageFileId, testDatabase);
                await this.target.HandleAsync(Command);
                await testDatabase.TakeSnapshotAsync();

                await this.target.HandleAsync(Command);

                return ExpectedSideEffects.None;
            });
        }

        [TestMethod]
        public async Task ItShouldUpdateSubscription()
        {
            await this.NewTestDatabaseAsync(async testDatabase =>
            {
                this.target = new UpdateSubscriptionCommandHandler(this.subscriptionSecurity.Object, this.fileSecurity.Object, testDatabase.NewContext());
                var subscription = await this.CreateSubscriptionAsync(UserId, SubscriptionId, HeaderImageFileId, testDatabase);
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
                    Update = expectedSubscription,
                    ExcludedFromTest = entity => entity is Channel
                };
            });
        }

        private async Task<Subscription> CreateSubscriptionAsync(UserId newUserId, SubscriptionId newSubscriptionId, FileId headerImageFileId, TestDatabaseContext testDatabase)
        {
            using (var databaseContext = testDatabase.NewContext())
            {
                await databaseContext.CreateTestSubscriptionAsync(newUserId.Value, newSubscriptionId.Value, headerImageFileId.Value);
            }

            using (var databaseContext = testDatabase.NewContext())
            {
                return await databaseContext.Subscriptions.SingleAsync(_ => _.Id == newSubscriptionId.Value);
            }
        }
    }
}