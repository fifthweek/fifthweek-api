namespace Fifthweek.Api.Identity.Tests.OAuth
{
    using System;
    using System.IO;
    using System.Security.Claims;
    using System.Security.Principal;
    using System.Web;

    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.Api.Identity.OAuth;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Tests.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class UserContextTests
    {
        private static readonly UserId UserId = new UserId(Guid.NewGuid());

        private const string AuthenticationType = "bearer";

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void WhenAuthenticatedButNoNameIdentifierClaimExists_GetRequesterShouldThrowAnException()
        {
            HttpContext.Current.User = new Principal(AuthenticationType);
            Assert.AreEqual(Requester.Unauthenticated, this.userContext.GetRequester());
        }

        [TestMethod]
        public void WhenNoHttpContext_RequesterShouldBeUnauthenticated()
        {
            HttpContext.Current = null;
            Assert.AreEqual(Requester.Unauthenticated, this.userContext.GetRequester());
        }

        [TestMethod]
        public void WhenNoUser_RequesterShouldBeUnauthenticated()
        {
            HttpContext.Current.User = null;
            Assert.AreEqual(Requester.Unauthenticated, this.userContext.GetRequester());
        }

        [TestMethod]
        public void WhenNoIdentity_RequesterShouldBeUnauthenticated()
        {
            HttpContext.Current.User = new NullIdentityPrincipal();
            Assert.AreEqual(Requester.Unauthenticated, this.userContext.GetRequester());
        }

        [TestMethod]
        public void WhenNoAuthenticationType_RequesterShouldBeUnauthenticated()
        {
            HttpContext.Current.User = new Principal(null);
            Assert.AreEqual(Requester.Unauthenticated, this.userContext.GetRequester());
        }

        [TestMethod]
        public void WhenUserIdClaimExists_RequesterShouldBeAuthenticated()
        {
            var principal = new Principal(AuthenticationType);
            principal.ClaimsIdentity.AddClaim(new Claim(ClaimTypes.NameIdentifier, UserId.Value.ToString()));

            HttpContext.Current.User = principal;

            var result = this.userContext.GetRequester();

            Assert.AreEqual(Requester.Authenticated(UserId), result);
        }

        [TestMethod]
        public void WhenNotAuthenticatedButNameIdentifierClamExists_RequesterShouldBeUnauthenticated()
        {
            var principal = new Principal(null);
            principal.ClaimsIdentity.AddClaim(new Claim(ClaimTypes.NameIdentifier, UserId.Value.ToString()));

            HttpContext.Current.User = principal;

            var result = this.userContext.GetRequester();

            Assert.AreEqual(Requester.Unauthenticated, result);
        }

        [TestMethod]
        public void IsInRoleShouldReturnTrueIfTheAuthenticatedUserIsInRole()
        {
            var principal = new Principal(AuthenticationType);
            principal.ClaimsIdentity.AddClaim(new Claim(ClaimTypes.NameIdentifier, UserId.Value.ToString()));
            principal.ClaimsIdentity.AddClaim(new Claim(ClaimTypes.Role, "One"));
            principal.ClaimsIdentity.AddClaim(new Claim(ClaimTypes.Role, "Two"));
            principal.ClaimsIdentity.AddClaim(new Claim(ClaimTypes.Role, "Three"));

            HttpContext.Current.User = principal;

            var result = this.userContext.GetRequester();

            Assert.IsTrue(result.IsInRole("Two"));
            Assert.IsFalse(result.IsInRole("Four"));
        }

        [TestMethod]
        public void IsInRoleShouldReturnFalseIfNotAuthenticated()
        {
            var principal = new Principal(null);
            principal.ClaimsIdentity.AddClaim(new Claim(ClaimTypes.NameIdentifier, UserId.Value.ToString()));
            principal.ClaimsIdentity.AddClaim(new Claim(ClaimTypes.Role, "One"));
            principal.ClaimsIdentity.AddClaim(new Claim(ClaimTypes.Role, "Two"));
            principal.ClaimsIdentity.AddClaim(new Claim(ClaimTypes.Role, "Three"));

            HttpContext.Current.User = principal;

            var result = this.userContext.GetRequester();

            Assert.IsFalse(result.IsInRole("Two"));
        }

        [TestInitialize]
        public void TestInitialize()
        {
            TextWriter tw = new StreamWriter(new MemoryStream());
            HttpContext.Current = new HttpContext(new HttpRequest("blah", "http://blah.com", "blah"), new HttpResponse(tw));

            this.userContext = new UserContext();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            HttpContext.Current = null;
        }

        private UserContext userContext;

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