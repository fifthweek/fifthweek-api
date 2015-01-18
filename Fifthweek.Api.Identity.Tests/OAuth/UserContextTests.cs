namespace Fifthweek.Api.Identity.Tests.OAuth
{
    using System;
    using System.IO;
    using System.Security.Claims;
    using System.Security.Principal;
    using System.Web;

    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.Api.Identity.OAuth;
    using Fifthweek.Tests.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class UserContextTests
    {
        private const string AuthenticationType = "bearer";

        [TestMethod]
        public void IsAuthenticatedShouldReturnTrueIfAuthenticated()
        {
            HttpContext.Current.User = new Principal(AuthenticationType);
            Assert.IsTrue(this.userContext.IsAuthenticated);
        }

        [TestMethod]
        public void IsAuthenticatedShouldReturnFalseIfNoHttpContext()
        {
            HttpContext.Current = null;
            Assert.IsFalse(this.userContext.IsAuthenticated);
        }

        [TestMethod]
        public void IsAuthenticatedShouldReturnFalseIfNoUser()
        {
            HttpContext.Current.User = null;
            Assert.IsFalse(this.userContext.IsAuthenticated);
        }

        [TestMethod]
        public void IsAuthenticatedShouldReturnFalseIfNoIdentity()
        {
            HttpContext.Current.User = new NullIdentityPrincipal();
            Assert.IsFalse(this.userContext.IsAuthenticated);
        }

        [TestMethod]
        public void IsAuthenticatedShouldReturnFalseIfNoAuthenticationType()
        {
            HttpContext.Current.User = new Principal(null);
            Assert.IsFalse(this.userContext.IsAuthenticated);
        }

        [TestMethod]
        public void GetUsernameShouldThrowExceptionIfNotAuthenticated()
        {
            var principal = new Principal(null);
            var un = "blah";
            principal.ClaimsIdentity.AddClaim(new Claim(ClaimTypes.Name, un));

            HttpContext.Current.User = principal;

            ExpectedException.AssertException<InvalidOperationException>(() =>
            {
                this.userContext.GetUsername();
            });
        }

        [TestMethod]
        public void TryGetUsernameShouldReturnTheAuthenticatedUsername()
        {
            var principal = new Principal(AuthenticationType);
            var un = "blah";
            principal.ClaimsIdentity.AddClaim(new Claim(ClaimTypes.Name, un));

            HttpContext.Current.User = principal;

            var result = this.userContext.TryGetUsername();

            Assert.AreEqual(un, result);
        }

        [TestMethod]
        public void TryGetUsernameShouldReturnNullIfNotAuthenticated()
        {
            var principal = new Principal(null);
            var un = "blah";
            principal.ClaimsIdentity.AddClaim(new Claim(ClaimTypes.Name, un));

            HttpContext.Current.User = principal;

            var result = this.userContext.TryGetUsername();

            Assert.IsNull(result);
        }

        [TestMethod]
        public void GetUserIdShouldThrowExceptionIfNotAuthenticated()
        {
            var principal = new Principal(null);
            var id = Guid.NewGuid();
            principal.ClaimsIdentity.AddClaim(new Claim(ClaimTypes.NameIdentifier, id.ToString()));

            HttpContext.Current.User = principal;

            ExpectedException.AssertException<InvalidOperationException>(() =>
            {
                this.userContext.GetUserId();
            });
        }

        [TestMethod]
        public void TryGetUserIdShouldReturnTheAuthenticatedUserId()
        {
            var principal = new Principal(AuthenticationType);
            var id = Guid.NewGuid();
            principal.ClaimsIdentity.AddClaim(new Claim(ClaimTypes.NameIdentifier, id.ToString()));

            HttpContext.Current.User = principal;

            var result = this.userContext.TryGetUserId();

            Assert.AreEqual(new UserId(id), result);
        }

        [TestMethod]
        public void TryGetUserIdShouldReturnNullIfNotAuthenticated()
        {
            var principal = new Principal(null);
            var id = Guid.NewGuid();
            principal.ClaimsIdentity.AddClaim(new Claim(ClaimTypes.NameIdentifier, id.ToString()));

            HttpContext.Current.User = principal;

            var result = this.userContext.TryGetUserId();

            Assert.IsNull(result);
        }

        [TestMethod]
        public void IsUserInRoleShouldReturnTrueIfTheAuthenticatedUserIsInRole()
        {
            var principal = new Principal(AuthenticationType);
            principal.ClaimsIdentity.AddClaim(new Claim(ClaimTypes.Role, "One"));
            principal.ClaimsIdentity.AddClaim(new Claim(ClaimTypes.Role, "Two"));
            principal.ClaimsIdentity.AddClaim(new Claim(ClaimTypes.Role, "Three"));

            HttpContext.Current.User = principal;

            Assert.IsTrue(this.userContext.IsUserInRole("Two"));
            Assert.IsFalse(this.userContext.IsUserInRole("Four"));
        }

        [TestMethod]
        public void IsUserInRoleShouldReturnFalseIfNotAuthenticated()
        {
            var principal = new Principal(null);
            principal.ClaimsIdentity.AddClaim(new Claim(ClaimTypes.Role, "One"));
            principal.ClaimsIdentity.AddClaim(new Claim(ClaimTypes.Role, "Two"));
            principal.ClaimsIdentity.AddClaim(new Claim(ClaimTypes.Role, "Three"));

            HttpContext.Current.User = principal;

            Assert.IsFalse(this.userContext.IsUserInRole("Two"));
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