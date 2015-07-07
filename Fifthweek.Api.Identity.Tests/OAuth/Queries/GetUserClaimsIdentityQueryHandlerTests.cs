namespace Fifthweek.Api.Identity.Tests.OAuth.Queries
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using System.Web.Security;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.OAuth;
    using Fifthweek.Api.Identity.OAuth.Queries;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class GetUserClaimsIdentityQueryHandlerTests
    {
        private static readonly Username Username = new Username("username");
        private static readonly Password Password = new Password("password");
        public static readonly UserId UserId = new UserId(Guid.NewGuid());

        private static readonly UserIdAndRoles UserIdAndRoles = new UserIdAndRoles(
            UserId,
            new List<string> { "One", "Two" });

        private static readonly UsernameAndRoles UsernameAndRoles = new UsernameAndRoles(
            Username,
            new List<string> { "One", "Two" });

        private static readonly string AuthenticationType = "bearer";


        private GetUserClaimsIdentityQueryHandler target;
        private Mock<IGetUserAndRolesFromCredentialsDbStatement> getUserAndRolesFromCredentials;
        private Mock<IGetUserAndRolesFromUserIdDbStatement> getUserAndRolesFromUserId;

        [TestInitialize]
        public void TestInitialize()
        {
            this.getUserAndRolesFromCredentials = new Mock<IGetUserAndRolesFromCredentialsDbStatement>(MockBehavior.Strict);
            this.getUserAndRolesFromUserId = new Mock<IGetUserAndRolesFromUserIdDbStatement>(MockBehavior.Strict);
            this.target = new GetUserClaimsIdentityQueryHandler(this.getUserAndRolesFromCredentials.Object, this.getUserAndRolesFromUserId.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenQueryIsNull_ItShouldThrowAnException()
        {
            await this.target.HandleAsync(null);
        }

        [TestMethod]
        [ExpectedException(typeof(BadRequestException))]
        public async Task WhenAuthenticationTypeIsEmpty_ItShouldThrowAnException()
        {
            await this.target.HandleAsync(new GetUserClaimsIdentityQuery(null, Username, Password, " "));
        }

        [TestMethod]
        [ExpectedException(typeof(BadRequestException))]
        public async Task WhenAuthenticationTypeIsEmpty_ItShouldThrowAnException2()
        {
            await this.target.HandleAsync(new GetUserClaimsIdentityQuery(UserId, null, null, " "));
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public async Task WhenUserIdAndUsernameProvided_ItShouldThrowAnException()
        {
            await this.target.HandleAsync(new GetUserClaimsIdentityQuery(UserId, Username, null, AuthenticationType));
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public async Task WhenUserIdAndPasswordProvided_ItShouldThrowAnException()
        {
            await this.target.HandleAsync(new GetUserClaimsIdentityQuery(UserId, null, Password, AuthenticationType));
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public async Task WhenUserIdAndUsernameAndPasswordProvided_ItShouldThrowAnException()
        {
            await this.target.HandleAsync(new GetUserClaimsIdentityQuery(UserId, Username, Password, AuthenticationType));
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public async Task WhenNeitherUserIdNorUsernameNorPasswordProvided_ItShouldThrowAnException()
        {
            await this.target.HandleAsync(new GetUserClaimsIdentityQuery(null, null, null, AuthenticationType));
        }

        [TestMethod]
        [ExpectedException(typeof(BadRequestException))]
        public async Task WhenCredentialsAreInValid_ItShouldThrowAnException()
        {
            this.getUserAndRolesFromCredentials.Setup(v => v.ExecuteAsync(Username, Password))
                .ReturnsAsync(null);

            await this.target.HandleAsync(new GetUserClaimsIdentityQuery(null, Username, Password, AuthenticationType));
        }

        [TestMethod]
        [ExpectedException(typeof(UnauthorizedException))]
        public async Task WhenUserIdIsInValid_ItShouldThrowAnException()
        {
            this.getUserAndRolesFromUserId.Setup(v => v.ExecuteAsync(UserId))
                .ReturnsAsync(null);

            await this.target.HandleAsync(new GetUserClaimsIdentityQuery(UserId, null, null, AuthenticationType));
        }

        [TestMethod]
        public async Task WhenCredentialsAreValid_ItShouldReturnAClaimsIdentity()
        {
            this.getUserAndRolesFromCredentials.Setup(v => v.ExecuteAsync(Username, Password))
                .ReturnsAsync(UserIdAndRoles);

            var result = await this.target.HandleAsync(new GetUserClaimsIdentityQuery(null, Username, Password, AuthenticationType));

            Assert.IsNotNull(result);
            Assert.AreEqual(UserId, result.UserId);
            Assert.AreEqual(AuthenticationType, result.ClaimsIdentity.AuthenticationType);
            Assert.AreEqual(Username.Value, result.ClaimsIdentity.Name);
            Assert.AreEqual(UserId.Value.EncodeGuid(), result.ClaimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value);

            var resultRoles = result.ClaimsIdentity.FindAll(ClaimTypes.Role).ToList();
            Assert.AreEqual(UserIdAndRoles.Roles.Count, resultRoles.Count);
            foreach (var role in UserIdAndRoles.Roles)
            {
                Assert.IsTrue(resultRoles.Any(v => v.Value == role));
            }
        }

        [TestMethod]
        public async Task WhenUserIdIsValid_ItShouldReturnAClaimsIdentity()
        {
            this.getUserAndRolesFromUserId.Setup(v => v.ExecuteAsync(UserId))
                .ReturnsAsync(UsernameAndRoles);

            var result = await this.target.HandleAsync(new GetUserClaimsIdentityQuery(UserId, null, null, AuthenticationType));

            Assert.IsNotNull(result);
            Assert.AreEqual(UserId, result.UserId);
            Assert.AreEqual(AuthenticationType, result.ClaimsIdentity.AuthenticationType);
            Assert.AreEqual(Username.Value, result.ClaimsIdentity.Name);
            Assert.AreEqual(UserId.Value.EncodeGuid(), result.ClaimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value);

            var resultRoles = result.ClaimsIdentity.FindAll(ClaimTypes.Role).ToList();
            Assert.AreEqual(UserIdAndRoles.Roles.Count, resultRoles.Count);
            foreach (var role in UserIdAndRoles.Roles)
            {
                Assert.IsTrue(resultRoles.Any(v => v.Value == role));
            }
        }
    }
}