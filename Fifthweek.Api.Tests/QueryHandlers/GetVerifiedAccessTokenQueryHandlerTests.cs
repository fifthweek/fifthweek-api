namespace Fifthweek.Api.Tests.QueryHandlers
{
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;

    using Fifthweek.Api.Queries;
    using Fifthweek.Api.QueryHandlers;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using RichardSzalay.MockHttp;

    [TestClass]
    public class GetVerifiedAccessTokenQueryHandlerTests
    {
        [TestMethod]
        public async Task ItShouldReturnAParsedToken()
        {
            var mockHttp = new MockHttpMessageHandler();

            var responseJson = "{'data': {'user_id': 'UserId', 'app_id': '"
                               + OAuthConfig.FacebookAppId + "' }}";

            mockHttp.When("https://graph.facebook.com/*")
                .Respond("application/json", responseJson);

            var httpClient = new HttpClient(mockHttp);

            var handler = new GetVerifiedAccessTokenQueryHandler(httpClient);
            var query = new GetVerifiedAccessTokenQuery("Facebook", "ABC");

            var result = await handler.HandleAsync(query);

            Assert.IsNotNull(result);
            Assert.AreEqual("UserId", result.UserId);
            Assert.AreEqual(OAuthConfig.FacebookAppId, result.ApplicationId);
        }

        [TestMethod]
        public async Task ItShouldReturnNullForAnUnrecognisedProvider()
        {
            var mockHttp = new MockHttpMessageHandler();
            var httpClient = new HttpClient(mockHttp);

            var handler = new GetVerifiedAccessTokenQueryHandler(httpClient);
            var query = new GetVerifiedAccessTokenQuery("XYZ", "ABC");

            var result = await handler.HandleAsync(query);

            Assert.IsNull(result);
        }

        [TestMethod]
        public async Task ItShouldReturnNullIfStatusCodeIsNotSuccess()
        {
            var mockHttp = new MockHttpMessageHandler();

            mockHttp.When("https://graph.facebook.com/*")
                .Respond(HttpStatusCode.BadRequest);

            var httpClient = new HttpClient(mockHttp);

            var handler = new GetVerifiedAccessTokenQueryHandler(httpClient);
            var query = new GetVerifiedAccessTokenQuery("Facebook", "ABC");

            var result = await handler.HandleAsync(query);

            Assert.IsNull(result);
        }
    }
}