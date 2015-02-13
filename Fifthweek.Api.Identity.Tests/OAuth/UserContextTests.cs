namespace Fifthweek.Api.Identity.Tests.OAuth
{
    using System;
    using System.IO;
    using System.Security.Claims;
    using System.Security.Principal;
    using System.Web;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.Api.Identity.OAuth;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Tests.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class UserContextTests
    {
        private static readonly UserId UserId = new UserId(Guid.NewGuid());

        private const string AuthenticationType = "bearer";

        private MockRequestContext mockRequestContext;
        private RequesterContext requesterContext;

        [TestInitialize]
        public void TestInitialize()
        {
            this.mockRequestContext = new MockRequestContext();
            TextWriter tw = new StreamWriter(new MemoryStream());
            this.mockRequestContext.HttpContext = new HttpContext(new HttpRequest("blah", "http://blah.com", "blah"), new HttpResponse(tw));
            
            this.requesterContext = new RequesterContext(this.mockRequestContext);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            this.mockRequestContext.HttpContext = null;
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void WhenAuthenticatedButNoNameIdentifierClaimExists_GetRequesterShouldThrowAnException()
        {
            this.mockRequestContext.HttpContext.User = new Principal(AuthenticationType);
            Assert.AreEqual(Requester.Unauthenticated, this.requesterContext.GetRequester());
        }

        [TestMethod]
        public void WhenNoHttpContext_RequesterShouldBeUnauthenticated()
        {
            this.mockRequestContext.HttpContext = null;
            Assert.AreEqual(Requester.Unauthenticated, this.requesterContext.GetRequester());
        }

        [TestMethod]
        public void WhenNoUser_RequesterShouldBeUnauthenticated()
        {
            this.mockRequestContext.HttpContext.User = null;
            Assert.AreEqual(Requester.Unauthenticated, this.requesterContext.GetRequester());
        }

        [TestMethod]
        public void WhenNoIdentity_RequesterShouldBeUnauthenticated()
        {
            this.mockRequestContext.HttpContext.User = new NullIdentityPrincipal();
            Assert.AreEqual(Requester.Unauthenticated, this.requesterContext.GetRequester());
        }

        [TestMethod]
        public void WhenNoAuthenticationType_RequesterShouldBeUnauthenticated()
        {
            this.mockRequestContext.HttpContext.User = new Principal(null);
            Assert.AreEqual(Requester.Unauthenticated, this.requesterContext.GetRequester());
        }

        [TestMethod]
        public void WhenUserIdClaimExists_RequesterShouldBeAuthenticated()
        {
            var principal = new Principal(AuthenticationType);
            principal.ClaimsIdentity.AddClaim(new Claim(ClaimTypes.NameIdentifier, UserId.Value.EncodeGuid()));

            this.mockRequestContext.HttpContext.User = principal;

            var result = this.requesterContext.GetRequester();

            Assert.AreEqual(Requester.Authenticated(UserId), result);
        }

        [TestMethod]
        public void WhenNotAuthenticatedButNameIdentifierClamExists_RequesterShouldBeUnauthenticated()
        {
            var principal = new Principal(null);
            principal.ClaimsIdentity.AddClaim(new Claim(ClaimTypes.NameIdentifier, UserId.Value.EncodeGuid()));

            this.mockRequestContext.HttpContext.User = principal;

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

            this.mockRequestContext.HttpContext.User = principal;

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

            this.mockRequestContext.HttpContext.User = principal;

            var result = this.requesterContext.GetRequester();

            Assert.IsFalse(result.IsInRole("Two"));
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

        private class MockRequestContext : IRequestContext
        {
            public HttpContext HttpContext { get; set; }
        }
    }
}