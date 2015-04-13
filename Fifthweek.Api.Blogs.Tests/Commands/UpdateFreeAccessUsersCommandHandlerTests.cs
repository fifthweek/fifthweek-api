namespace Fifthweek.Api.Blogs.Tests.Commands
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Fifthweek.Api.Blogs.Commands;
    using Fifthweek.Api.Blogs.Queries;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Identity.Tests.Shared.Membership;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class UpdateFreeAccessUsersCommandHandlerTests
    {
        private static readonly List<ValidEmail> InputEmailAddresses = new List<ValidEmail> { ValidEmail.Parse("a@b.com"), ValidEmail.Parse("b@b.com"), ValidEmail.Parse("a@b.com") };
        private static readonly List<ValidEmail> UniqueEmailAddresses = new List<ValidEmail> { ValidEmail.Parse("a@b.com"), ValidEmail.Parse("b@b.com") };

        private static readonly UserId UserId = new UserId(Guid.NewGuid());

        private static readonly UpdateFreeAccessUsersCommand Command =
            new UpdateFreeAccessUsersCommand(
                Requester.Authenticated(UserId),
                new BlogId(Guid.NewGuid()),
                InputEmailAddresses);

        private Mock<IBlogSecurity> blogSecurity;
        private Mock<IRequesterSecurity> requesterSecurity;
        private Mock<IUpdateFreeAccessUsersDbStatement> updateFreeAccessUsers;

        private UpdateFreeAccessUsersCommandHandler target;

        [TestInitialize]
        public void Initialize()
        {
            this.blogSecurity = new Mock<IBlogSecurity>();
            this.requesterSecurity = new Mock<IRequesterSecurity>();
            this.requesterSecurity.SetupFor(Command.Requester);
            this.updateFreeAccessUsers = new Mock<IUpdateFreeAccessUsersDbStatement>(MockBehavior.Strict);

            this.blogSecurity.Setup(v => v.AssertWriteAllowedAsync(UserId, Command.BlogId))
                .Returns(Task.FromResult(0))
                .Verifiable();

            this.target = new UpdateFreeAccessUsersCommandHandler(
                this.blogSecurity.Object,
                this.requesterSecurity.Object,
                this.updateFreeAccessUsers.Object);
        }

        private void SetupDbStatement()
        {
            this.updateFreeAccessUsers.Setup(v => v.ExecuteAsync(Command.BlogId, UniqueEmailAddresses))
                .Returns(Task.FromResult(0))
                .Verifiable();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenQueryIsNull_ItShouldThrowAnException()
        {
            await this.target.HandleAsync(null);
        }

        [TestMethod]
        [ExpectedException(typeof(UnauthorizedException))]
        public async Task WhenUserIsUnautorized_ItShouldThrowAnException()
        {
            await this.target.HandleAsync(new UpdateFreeAccessUsersCommand(
                Requester.Unauthenticated,
                Command.BlogId,
                Command.EmailAddresses));
        }

        [TestMethod]
        public async Task WhenQueryIsValid_ItShouldCheckTheAuthenticatedUserOwnsTheBlog()
        {
            this.SetupDbStatement();
            await this.target.HandleAsync(Command);
            this.blogSecurity.Verify();
        }

        [TestMethod]
        public async Task WhenQueryIsValid_ItShouldGetFreeAccessUsersPassingUniqueEmailAddresses()
        {
            this.SetupDbStatement();
            await this.target.HandleAsync(Command);
            this.updateFreeAccessUsers.Verify();
        }
    }
}