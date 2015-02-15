namespace Fifthweek.Api.Core.Tests
{
    using System;
    using System.Net.Http;
    using System.Web.Http.Controllers;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class RequestContextTests
    {
        private RequestContext target;

        [TestInitialize]
        public void TestInitialize()
        {
            this.target = new RequestContext();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void WhenRequestIsAccessedBeforeInitializing_ItShouldThrowAnException()
        {
            var request = this.target.Request;
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void WhenContextIsAccessedBeforeInitializing_ItShouldThrowAnException()
        {
            var context = this.target.Context;
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void WhenInitializedTwice_ItShouldThrowAnException()
        {
            this.target.Initialize(new HttpRequestMessage(), new HttpRequestContext());
            this.target.Initialize(new HttpRequestMessage(), new HttpRequestContext());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void WhenInitializedWithNullRequest_ItShouldThrowAnException()
        {
            this.target.Initialize(null, new HttpRequestContext());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void WhenInitializedWithNullContext_ItShouldThrowAnException()
        {
            this.target.Initialize(new HttpRequestMessage(), null);
        }

        [TestMethod]
        public void WhenInitialized_ItShouldReturnTheRequest()
        {
            var request = new HttpRequestMessage();
            this.target.Initialize(request, new HttpRequestContext());
            var result = this.target.Request;
            Assert.AreSame(request, result);
        }

        [TestMethod]
        public void WhenInitialized_ItShouldReturnTheContext()
        {
            var context = new HttpRequestContext();
            this.target.Initialize(new HttpRequestMessage(), context);
            var result = this.target.Context;
            Assert.AreSame(context, result);
        }
    }
}