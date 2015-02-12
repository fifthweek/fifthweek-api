namespace Fifthweek.Api.Tests
{
    using System;
    using System.IO;
    using System.Net;
    using System.Net.Http.Headers;
    using System.Security.Claims;
    using System.Security.Principal;
    using System.Web;
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
            HttpContext.Current = new HttpContext(new HttpRequest("blah", "http://blah.com", "blah"), new HttpResponse(tw));
        }

        [TestMethod]
        public void WhenUserIsAuthenticated_ItShouldDoNothing()
        {
            HttpContext.Current.User = new Principal(AuthenticationType);

            var context = new System.Web.Http.Controllers.HttpActionContext();
            this.target.OnActionExecuting(context);

            Assert.IsNull(context.Response);
        }

        [TestMethod]
        public void WhenUserIsNotAuthenticatedAndNoAuthorizationExists_ItShouldDoNothing()
        {
            HttpContext.Current.User = new Principal(null);
            var context = this.CreateHttpActionContext();
            
            this.target.OnActionExecuting(context);

            Assert.IsNull(context.Response);
        }

        [TestMethod]
        public void WhenUserIdentityIsNullAndNoAuthorizationExists_ItShouldDoNothing()
        {
            HttpContext.Current.User = new NullIdentityPrincipal();
            var context = this.CreateHttpActionContext();
            
            this.target.OnActionExecuting(context);

            Assert.IsNull(context.Response);
        }

        [TestMethod]
        public void WhenUserIsNotAuthenticatedAndAuthorizationExists_ItShouldSetTheResponseToUnauthorized()
        {
            HttpContext.Current.User = new Principal(null);

            var context = this.CreateHttpActionContext();
            context.Request.Headers.Authorization = new AuthenticationHeaderValue(AuthenticationType);

            this.target.OnActionExecuting(context);

            Assert.IsNotNull(context.Response);
            Assert.AreEqual(HttpStatusCode.Unauthorized, context.Response.StatusCode);
        }

        private HttpActionContext CreateHttpActionContext()
        {
            var context =
                new System.Web.Http.Controllers.HttpActionContext(
                    new System.Web.Http.Controllers.HttpControllerContext(
                        new System.Web.Http.HttpConfiguration(),
                        new HttpRouteData(new HttpRoute()),
                        new System.Net.Http.HttpRequestMessage()),
                    new ReflectedHttpActionDescriptor());
            return context;
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