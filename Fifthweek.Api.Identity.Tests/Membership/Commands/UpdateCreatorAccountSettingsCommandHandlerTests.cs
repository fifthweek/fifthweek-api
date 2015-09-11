namespace Fifthweek.Api.Identity.Tests.Membership.Commands
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Dapper;

    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Membership.Commands;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Identity.Tests.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Api.Persistence.Tests.Shared;

    using Microsoft.AspNet.Identity;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class UpdateCreatorAccountSettingsCommandHandlerTests : PersistenceTestsBase
    {
        private static readonly UserId UserId = new UserId(Guid.NewGuid());
        private static readonly Requester Requester = Requester.Authenticated(UserId);
        private static readonly UpdateCreatorAccountSettingsCommand Command = new UpdateCreatorAccountSettingsCommand(Requester, UserId);
        private Mock<IUserManager> userManager;
        private Mock<IRequesterSecurity> requesterSecurity;
        private Mock<IFifthweekDbConnectionFactory> connectionFactory;
        private UpdateCreatorAccountSettingsCommandHandler target;

        [TestInitialize]
        public void Initialize()
        {
            this.requesterSecurity = new Mock<IRequesterSecurity>();
            this.requesterSecurity.SetupFor(Requester);
            this.userManager = new Mock<IUserManager>(MockBehavior.Strict);
            this.connectionFactory = new Mock<IFifthweekDbConnectionFactory>(MockBehavior.Strict);
            this.target = new UpdateCreatorAccountSettingsCommandHandler(this.requesterSecurity.Object, this.userManager.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenCommandIsNull_ItShouldThrowAnException()
        {
            await this.target.HandleAsync(null);
        }

        [TestMethod]
        [ExpectedException(typeof(UnauthorizedException))]
        public async Task WhenUnauthenticated_ItShouldThrowUnauthorizedException()
        {
            await this.target.HandleAsync(new UpdateCreatorAccountSettingsCommand(
                Requester.Unauthenticated,
                UserId));
        }

        [TestMethod]
        [ExpectedException(typeof(UnauthorizedException))]
        public async Task WhenNotTheAuthenticatedUser_ItShouldThrowUnauthorizedException()
        {
            await this.target.HandleAsync(new UpdateCreatorAccountSettingsCommand(
                Requester,
                new UserId(Guid.NewGuid())));
        }

        [TestMethod]
        public async Task WhenUserIsNotACreator_ItShouldUpdateSettingsAddTheUserToCreatorRole()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new UpdateCreatorAccountSettingsCommandHandler(this.requesterSecurity.Object, this.userManager.Object);

                var expectedUser = await this.CreateUserAsync(testDatabase);
                await testDatabase.TakeSnapshotAsync();

                this.requesterSecurity.Setup(v => v.IsInRoleAsync(Requester, FifthweekRole.Creator)).ReturnsAsync(false);
                this.userManager.Setup(_ => _.AddToRoleAsync(UserId.Value, FifthweekRole.Creator)).ReturnsAsync(new IdentityResult()).Verifiable();

                await this.target.HandleAsync(Command);

                this.userManager.Verify();

                return ExpectedSideEffects.None;
            });
        }

        [TestMethod]
        public async Task WhenUserIsACreator_ItShouldUpdateSettings()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new UpdateCreatorAccountSettingsCommandHandler(this.requesterSecurity.Object, this.userManager.Object);

                var expectedUser = await this.CreateUserAsync(testDatabase);
                await testDatabase.TakeSnapshotAsync();

                this.requesterSecurity.Setup(v => v.IsInRoleAsync(Requester, FifthweekRole.Creator)).ReturnsAsync(true);

                await this.target.HandleAsync(Command);

                return ExpectedSideEffects.None;
            });
        }

        [TestMethod]
        public async Task ItShouldBeIdempotent()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new UpdateCreatorAccountSettingsCommandHandler(this.requesterSecurity.Object, this.userManager.Object);
                this.requesterSecurity.Setup(v => v.IsInRoleAsync(Requester, FifthweekRole.Creator)).ReturnsAsync(true);

                await this.CreateUserAsync(testDatabase);
                await this.target.HandleAsync(Command);
                await testDatabase.TakeSnapshotAsync();
                await this.target.HandleAsync(Command);

                return ExpectedSideEffects.None;
            });
        }

        private async Task<FifthweekUser> CreateUserAsync(TestDatabaseContext testDatabase)
        {
            var random = new Random();
            var user = UserTests.UniqueEntity(random);
            user.Id = UserId.Value;

            using (var databaseContext = testDatabase.CreateContext())
            {
                databaseContext.Users.Add(user);
                await databaseContext.SaveChangesAsync();
            }

            using (var connection = testDatabase.CreateConnection())
            {
                var users = await connection.QueryAsync<FifthweekUser>("SELECT * FROM AspNetUsers WHERE Id=@Id", new { Id = UserId.Value });
                return users.Single();
            }
        }
    }
}