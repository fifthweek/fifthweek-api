namespace Dexter.Api.Tests.QueryHandlers
{
    using System.Net.Http;
    using System.Threading.Tasks;

    using Dexter.Api.Queries;
    using Dexter.Api.QueryHandlers;

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
    }
}