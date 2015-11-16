namespace Fifthweek.Api.Identity.Membership.Tests
{
    using System;
    using System.Data.SqlTypes;
    using System.Threading.Tasks;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.Api.Persistence.Tests.Shared;
    using Fifthweek.Payments.Services;
    using Fifthweek.Tests.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class GetFeedbackUserDataDbStatementTests : PersistenceTestsBase
    {
        private static readonly DateTime Now = DateTime.UtcNow;

        private static readonly Username Username = new Username("username");
        private static readonly UserId UserId = new UserId(Guid.NewGuid());
        private static readonly FileId FileId = new FileId(Guid.NewGuid());
        private static readonly Email Email = new Email("accountrepositorytests@testing.fifthweek.com");

        private GetFeedbackUserDataDbStatement target;

        [TestInitialize]
        public void Initialize()
        {
            // Required for non-database tests.
            this.target = new GetFeedbackUserDataDbStatement(new Mock<IFifthweekDbConnectionFactory>(MockBehavior.Strict).Object);
        }

        [TestMethod]
        public async Task WhenGetAccountSettingsCalled_ItShouldGetAccountSettingsFromTheDatabase()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new GetFeedbackUserDataDbStatement(testDatabase);
                await this.CreateDataAsync(
                    testDatabase,
                    UserId,
                    Username,
                    Email);

                await testDatabase.TakeSnapshotAsync();

                var result = await this.target.ExecuteAsync(UserId);

                var expectedResult = new GetFeedbackUserDataDbStatement.GetFeedbackUserDataResult(
                    Email,
                    Username);

                Assert.AreEqual(expectedResult, result);
                
                return ExpectedSideEffects.None;
            });
        }

        [TestMethod]
        public async Task WhenGetAccountSettingsCalledWithInvalidUserId_ItShouldThrowAnException()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new GetFeedbackUserDataDbStatement(testDatabase);
                await this.CreateDataAsync(
                    testDatabase,
                    UserId,
                    Username,
                    Email);

                await testDatabase.TakeSnapshotAsync();

                Func<Task> badMethodCall = () => this.target.ExecuteAsync(new UserId(Guid.NewGuid()));

                await badMethodCall.AssertExceptionAsync<InvalidOperationException>();

                return ExpectedSideEffects.None;
            });
        }

        [TestMethod]
        public async Task WhenGetAccountSettingsCalledWithNullUserId_ItShouldThrowAnAugumentException()
        {
            Func<Task> badMethodCall = () => this.target.ExecuteAsync(null);

            await badMethodCall.AssertExceptionAsync<ArgumentNullException>();
        }

        private async Task CreateDataAsync(
            TestDatabaseContext testDatabase, 
            UserId userId, 
            Username username, 
            Email email)
        {
            var random = new Random();
            var user = UserTests.UniqueEntity(random);
            user.Id = userId.Value;
            user.Email = email.Value;
            user.UserName = username.Value;

            using (var databaseContext = testDatabase.CreateContext())
            {
                databaseContext.Users.Add(user);

                await databaseContext.SaveChangesAsync();
            }
        }
    }
}
