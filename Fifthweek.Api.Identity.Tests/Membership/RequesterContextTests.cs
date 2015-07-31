namespace Fifthweek.Api.Identity.Tests.Membership
{
    using System;
    using System.IO;
    using System.Net.Http;
    using System.Security.Claims;
    using System.Security.Principal;
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
        private RequesterContext requesterContext;

        [TestInitialize]
        public void TestInitialize()
        {
            this.requestContext = new Mock<IRequestContext>();
            this.requesterContext = new RequesterContext(this.requestContext.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void WhenAuthenticatedButNoNameIdentifierClaimExists_GetRequesterShouldThrowAnException()
        {
            this.SetupRequestContext(new Principal(AuthenticationType));
            Assert.AreEqual(Requester.Unauthenticated, this.requesterContext.GetRequester());
        }

        [TestMethod]
        public void WhenNoUser_RequesterShouldBeUnauthenticated()
        {
            this.SetupRequestContext(null);
            Assert.AreEqual(Requester.Unauthenticated, this.requesterContext.GetRequester());
        }

        [TestMethod]
        public void WhenNoIdentity_RequesterShouldBeUnauthenticated()
        {
            this.SetupRequestContext(new NullIdentityPrincipal());
            Assert.AreEqual(Requester.Unauthenticated, this.requesterContext.GetRequester());
        }

        [TestMethod]
        public void WhenNoAuthenticationType_RequesterShouldBeUnauthenticated()
        {
            this.SetupRequestContext(new Principal(null));
            Assert.AreEqual(Requester.Unauthenticated, this.requesterContext.GetRequester());
        }

        [TestMethod]
        public void WhenUserIdClaimExists_RequesterShouldBeAuthenticated()
        {
            var principal = new Principal(AuthenticationType);
            principal.ClaimsIdentity.AddClaim(new Claim(ClaimTypes.NameIdentifier, UserId.Value.EncodeGuid()));

            this.SetupRequestContext(principal);

            var result = this.requesterContext.GetRequester();

            Assert.AreEqual(Requester.Authenticated(UserId), result);
        }

        [TestMethod]
        public void WhenImpersonationInformationExists_RequesterShouldContainImpersonatedUserId()
        {
            var principal = new Principal(AuthenticationType);
            principal.ClaimsIdentity.AddClaim(new Claim(ClaimTypes.NameIdentifier, UserId.Value.EncodeGuid()));
            this.SetupRequestContext(principal);

            var impersonatedUserId = UserId.Random();
            this.SetupImpersonationHeader(impersonatedUserId);

            var result = this.requesterContext.GetRequester();

            Assert.AreEqual(Requester.Authenticated(UserId, impersonatedUserId), result);
        }

        [TestMethod]
        public void WhenNotAuthenticatedButNameIdentifierClamExists_RequesterShouldBeUnauthenticated()
        {
            var principal = new Principal(null);
            principal.ClaimsIdentity.AddClaim(new Claim(ClaimTypes.NameIdentifier, UserId.Value.EncodeGuid()));

            this.SetupRequestContext(principal);

            var result = this.requesterContext.GetRequester();

            Assert.AreEqual(Requester.Unauthenticated, result);
        }

        [TestMethod]
        public void IsInRoleShouldReturnTrueIfTheAuthenticatedUserIsInRole()
        {
            var principal = new Principal(AuthenticationType);
            principal.ClaimsIdentity.AddClaim(new Claim(ClaimTypes.NameIdentifier, UserId.Value.EncodeGuid()));
            principal.ClaimsIdentity.AddClaim(new Claim(ClaimTypes.Role, "One"));
            principal.ClaimsIdentity.AddClaim(new Claim(ClaimTypes.Role, "Two"));
            principal.ClaimsIdentity.AddClaim(new Claim(ClaimTypes.Role, "Three"));

            this.SetupRequestContext(principal);

            var result = this.requesterContext.GetRequester();

            Assert.IsTrue(result.IsInRole("Two"));
            Assert.IsFalse(result.IsInRole("Four"));
        }

        [TestMethod]
        public void IsInRoleShouldReturnFalseIfNotAuthenticated()
        {
            var principal = new Principal(null);
            principal.ClaimsIdentity.AddClaim(new Claim(ClaimTypes.NameIdentifier, UserId.Value.EncodeGuid()));
            principal.ClaimsIdentity.AddClaim(new Claim(ClaimTypes.Role, "One"));
            principal.ClaimsIdentity.AddClaim(new Claim(ClaimTypes.Role, "Two"));
            principal.ClaimsIdentity.AddClaim(new Claim(ClaimTypes.Role, "Three"));

            this.SetupRequestContext(principal);

            var result = this.requesterContext.GetRequester();

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