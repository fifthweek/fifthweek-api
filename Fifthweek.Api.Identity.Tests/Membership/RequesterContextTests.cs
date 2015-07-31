namespace Fifthweek.Api.Identity.Tests.Membership
{
    using System;
    using System.IO;
    using System.Net.Http;
    using System.Security.Claims;
    using System.Security.Principal;
    using System.Threading.Tasks;
    using System.Web;
    using System.Web.Http.Controllers;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.Api.Identity.OAuth;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Tests.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class RequesterContextTests
    {
        private static readonly UserId UserId = new UserId(Guid.NewGuid());

        private const string AuthenticationType = "bearer";

        private Mock<IRequestContext> requestContext;
        private Mock<IImpersonateIfRequired> impersonateIfRequired;
        private RequesterContext requesterContext;

        [TestInitialize]
        public void TestInitialize()
        {
            this.requestContext = new Mock<IRequestContext>();
            this.impersonateIfRequired = new Mock<IImpersonateIfRequired>(MockBehavior.Strict);
            this.requesterContext = new RequesterContext(this.requestContext.Object, this.impersonateIfRequired.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public async Task WhenAuthenticatedButNoNameIdentifierClaimExists_GetRequesterShouldThrowAnException()
        {
            this.SetupRequestContext(new Principal(AuthenticationType));

            Assert.AreEqual(Requester.Unauthenticated, await this.requesterContext.GetRequesterAsync());
        }

        [TestMethod]
        public async Task WhenNoUser_RequesterShouldBeUnauthenticated()
        {
            this.SetupRequestContext(null);
            Assert.AreEqual(Requester.Unauthenticated, await this.requesterContext.GetRequesterAsync());
        }

        [TestMethod]
        public async Task WhenNoIdentity_RequesterShouldBeUnauthenticated()
        {
            this.SetupRequestContext(new NullIdentityPrincipal());
            Assert.AreEqual(Requester.Unauthenticated, await this.requesterContext.GetRequesterAsync());
        }

        [TestMethod]
        public async Task WhenNoAuthenticationType_RequesterShouldBeUnauthenticated()
        {
            this.SetupRequestContext(new Principal(null));
            Assert.AreEqual(Requester.Unauthenticated, await this.requesterContext.GetRequesterAsync());
        }

        [TestMethod]
        public async Task WhenUserIdClaimExists_RequesterShouldBeAuthenticated()
        {
            var principal = new Principal(AuthenticationType);
            principal.ClaimsIdentity.AddClaim(new Claim(ClaimTypes.NameIdentifier, UserId.Value.EncodeGuid()));

            this.SetupRequestContext(principal);

            var result = await this.requesterContext.GetRequesterAsync();

            Assert.AreEqual(Requester.Authenticated(UserId), result);
        }

        [TestMethod]
        public async Task WhenImpersonationInformationExists_RequesterShouldContainImpersonatedUserId()
        {
            var principal = new Principal(AuthenticationType);
            principal.ClaimsIdentity.AddClaim(new Claim(ClaimTypes.NameIdentifier, UserId.Value.EncodeGuid()));
            this.SetupRequestContext(principal);

            var impersonatedUserId = UserId.Random();
            this.SetupImpersonationHeader(impersonatedUserId);

            var impersonatingRequester = Requester.Authenticated(impersonatedUserId, Requester.Authenticated(UserId));
            this.impersonateIfRequired.Setup(v => v.ExecuteAsync(Requester.Authenticated(UserId), impersonatedUserId))
                .ReturnsAsync(impersonatingRequester);

            var result = await this.requesterContext.GetRequesterAsync();

            Assert.AreEqual(impersonatingRequester, result);
        }

        [TestMethod]
        public async Task WhenImpersonationInformationExists_ButImpersonationNotRequired_ItShouldReturnUnimpersonatedRequester()
        {
            var principal = new Principal(AuthenticationType);
            principal.ClaimsIdentity.AddClaim(new Claim(ClaimTypes.NameIdentifier, UserId.Value.EncodeGuid()));
            this.SetupRequestContext(principal);

            var impersonatedUserId = UserId.Random();
            this.SetupImpersonationHeader(impersonatedUserId);

            this.impersonateIfRequired.Setup(v => v.ExecuteAsync(Requester.Authenticated(UserId), impersonatedUserId))
                .ReturnsAsync(null);

            var result = await this.requesterContext.GetRequesterAsync();

            Assert.AreEqual(Requester.Authenticated(UserId), result);
        }

        [TestMethod]
        public async Task WhenNotAuthenticatedButNameIdentifierClamExists_RequesterShouldBeUnauthenticated()
        {
            var principal = new Principal(null);
            principal.ClaimsIdentity.AddClaim(new Claim(ClaimTypes.NameIdentifier, UserId.Value.EncodeGuid()));

            this.SetupRequestContext(principal);

            var result = await this.requesterContext.GetRequesterAsync();

            Assert.AreEqual(Requester.Unauthenticated, result);
        }

        [TestMethod]
        public async Task IsInRoleShouldReturnTrueIfTheAuthenticatedUserIsInRole()
        {
            var principal = new Principal(AuthenticationType);
            principal.ClaimsIdentity.AddClaim(new Claim(ClaimTypes.NameIdentifier, UserId.Value.EncodeGuid()));
            principal.ClaimsIdentity.AddClaim(new Claim(ClaimTypes.Role, "One"));
            principal.ClaimsIdentity.AddClaim(new Claim(ClaimTypes.Role, "Two"));
            principal.ClaimsIdentity.AddClaim(new Claim(ClaimTypes.Role, "Three"));

            this.SetupRequestContext(principal);

            var result = await this.requesterContext.GetRequesterAsync();

            Assert.IsTrue(result.IsInRole("Two"));
            Assert.IsFalse(result.IsInRole("Four"));
        }

        [TestMethod]
        public async Task IsInRoleShouldReturnFalseIfNotAuthenticated()
        {
            var principal = new Principal(null);
            principal.ClaimsIdentity.AddClaim(new Claim(ClaimTypes.NameIdentifier, UserId.Value.EncodeGuid()));
            principal.ClaimsIdentity.AddClaim(new Claim(ClaimTypes.Role, "One"));
            principal.ClaimsIdentity.AddClaim(new Claim(ClaimTypes.Role, "Two"));
            principal.ClaimsIdentity.AddClaim(new Claim(ClaimTypes.Role, "Three"));

            this.SetupRequestContext(principal);

            var result = await this.requesterContext.GetRequesterAsync();

            Assert.IsFalse(result.IsInRole("Two"));
        }

        private void SetupRequestContext(IPrincipal principle)
        {
            var context = new HttpRequestContext() { Principal = principle };
            this.requestContext.Setup(v => v.Context).Returns(context);
        }

        private void SetupImpersonationHeader(UserId userId)
        {
            var message = new HttpRequestMessage();
            message.Headers.Add(RequesterContext.ImpersonateHeaderKey, userId.Value.EncodeGuid());
            this.requestContext.Setup(v => v.Request).Returns(message);
        }

        private class Principal : IPrincipal
        {
            public Principal(string authenticationType)
            {
                this.ClaimsIdentity = new ClaimsIdentity(authenticationType);
            }

            public ClaimsIdentity ClaimsIdentity { get; private set; }

            public IIdentity Identity
            {
                get
                {
                    return this.ClaimsIdentity;
                }
            }

            public bool IsInRole(string role)
            {
                throw new NotImplementedException();
            }
        }

        private class NullIdentityPrincipal : IPrincipal
        {
            public bool IsInRole(string role)
            {
                throw new NotImplementedException();
            }

            public IIdentity Identity 
            {
                get
                {
                    return null;
                }
            }
        }
    }
}