namespace Fifthweek.Api.Tests
{
    using System;
    using System.IO;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Security.Claims;
    using System.Security.Principal;
    using System.Web;
    using System.Web.Http;
    using System.Web.Http.Controllers;
    using System.Web.Http.Routing;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class InterceptExpiredBearerTokensAttributeTests
    {
        private const string AuthenticationType = "bearer";

        private InterceptExpiredBearerTokensAttribute target;

        [TestInitialize]
        public void TestInitialize()
        {
            this.target = new InterceptExpiredBearerTokensAttribute();
            TextWriter tw = new StreamWriter(new MemoryStream());
        }

        [TestMethod]
        public void WhenUserIsAuthenticated_ItShouldDoNothing()
        {
            var principle = new Principal(AuthenticationType);
            var context = this.CreateHttpActionContext(principle);
            
            this.target.OnActionExecuting(context);

            Assert.IsNull(context.Response);
        }

        [TestMethod]
        public void WhenUserIsNotAuthenticatedAndNoAuthorizationExists_ItShouldDoNothing()
        {
            var principle = new Principal(null);
            var context = this.CreateHttpActionContext(principle);
            
            this.target.OnActionExecuting(context);

            Assert.IsNull(context.Response);
        }

        [TestMethod]
        public void WhenUserIdentityIsNullAndNoAuthorizationExists_ItShouldDoNothing()
        {
            var principle = new NullIdentityPrincipal();
            var context = this.CreateHttpActionContext(principle);
            
            this.target.OnActionExecuting(context);

            Assert.IsNull(context.Response);
        }

        [TestMethod]
        public void WhenUserIsNotAuthenticatedAndAuthorizationExists_ItShouldSetTheResponseToUnauthorized()
        {
            var principle = new Principal(null);
            var context = this.CreateHttpActionContext(principle);
            context.Request.Headers.Authorization = new AuthenticationHeaderValue(AuthenticationType);

            this.target.OnActionExecuting(context);

            Assert.IsNotNull(context.Response);
            Assert.AreEqual(HttpStatusCode.Unauthorized, context.Response.StatusCode);
        }

        private HttpActionContext CreateHttpActionContext(IPrincipal principle)
        {
            var context =
                new System.Web.Http.Controllers.HttpActionContext(
                    new System.Web.Http.Controllers.HttpControllerContext(
                        new HttpRequestContext() { Principal = principle },
                        new HttpRequestMessage(),
                        new HttpControllerDescriptor(),
                        new Controller()),
                    new ReflectedHttpActionDescriptor());
            
            return context;
        }

        private class Controller : ApiController
        {
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