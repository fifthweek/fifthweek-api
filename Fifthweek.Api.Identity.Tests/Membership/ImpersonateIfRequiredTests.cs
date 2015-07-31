namespace Fifthweek.Api.Identity.Tests.Membership
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence.Identity;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class ImpersonateIfRequiredTests
    {
        private Mock<IRequesterSecurity> requesterSecurity;
        private Mock<IGetUserRolesDbStatement> getUserRoles;

        private ImpersonateIfRequired target;

        [TestInitialize]
        public void Initialize()
        {
            this.requesterSecurity = new Mock<IRequesterSecurity>(MockBehavior.Strict);
            this.getUserRoles = new Mock<IGetUserRolesDbStatement>(MockBehavior.Strict);

            this.target = new ImpersonateIfRequired(
                this.requesterSecurity.Object,
                this.getUserRoles.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenRequesterIsNull_ItShouldThrowAnException()
        {
            await this.target.ExecuteAsync(null, UserId.Random());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenUserIdIsNull_ItShouldThrowAnException()
        {
            await this.target.ExecuteAsync(Requester.Authenticated(UserId.Random()), null);
        }

        [TestMethod]
        [ExpectedException(typeof(UnauthenticatedException))]
        public async Task WhenRequesterIsUnauthenticated_ItShouldThrowAnException()
        {
            var userId = UserId.Random();
            var requester = Requester.Authenticated(userId);
            this.requesterSecurity.Setup(v => v.AuthenticateAsync(requester)).Throws(new UnauthenticatedException());

            await this.target.ExecuteAsync(requester, userId);
        }

        [TestMethod]
        public async Task WhenRequesterMatchesRequestedUserId_ItShouldReturnNull()
        {
            var userId = UserId.Random();
            var requester = Requester.Authenticated(userId);
            this.requesterSecurity.Setup(v => v.AuthenticateAsync(requester)).ReturnsAsync(userId);

            var result = await this.target.ExecuteAsync(requester, userId);

            Assert.IsNull(result);
        }

        [TestMethod]
        [ExpectedException(typeof(UnauthorizedException))]
        public async Task WhenUserIsNotAdministrator_ItShouldPurposelyFailAuthentication()
        {
            var userId = UserId.Random();
            var requestedUserId = UserId.Random();
            var requester = Requester.Authenticated(userId);
            this.requesterSecurity.Setup(v => v.AuthenticateAsync(requester)).ReturnsAsync(userId);

            this.requesterSecurity.Setup(v => v.IsInRoleAsync(requester, FifthweekRole.Administrator))
                .ReturnsAsync(false);

            this.requesterSecurity.Setup(v => v.AuthenticateAsAsync(requester, requestedUserId))
                .Throws(new UnauthorizedException());

            await this.target.ExecuteAsync(requester, requestedUserId);
        }

        [TestMethod]
        public async Task WhenUserIsAdministrator_ItShouldReturnImpersonatedRequester()
        {
            var userId = UserId.Random();
            var requestedUserId = UserId.Random();
            var requester = Requester.Authenticated(userId);
            this.requesterSecurity.Setup(v => v.AuthenticateAsync(requester)).ReturnsAsync(userId);

            this.requesterSecurity.Setup(v => v.IsInRoleAsync(requester, FifthweekRole.Administrator))
                .ReturnsAsync(true);

            this.getUserRoles.Setup(v => v.ExecuteAsync(requestedUserId)).ReturnsAsync(
                new GetUserRolesDbStatement.UserRoles(
                    new List<GetUserRolesDbStatement.UserRoles.UserRole>
                    {
                        new GetUserRolesDbStatement.UserRoles.UserRole(Guid.NewGuid(), "role1"),
                    }));

            var result = await this.target.ExecuteAsync(requester, requestedUserId);

            Assert.AreEqual(
                Requester.Authenticated(requestedUserId, requester, "role1"),
                result);
        }
    }
}