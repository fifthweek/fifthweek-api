namespace Dexter.Api.Tests.QueryHandlers
{
    using System.Threading.Tasks;

    using Dexter.Api.Entities;
    using Dexter.Api.Models;
    using Dexter.Api.Queries;
    using Dexter.Api.QueryHandlers;
    using Dexter.Api.Repositories;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class GetRefreshTokenQueryHandlerTests
    {
        [TestMethod]
        public async Task ItShouldReturnTheRequestedRefreshToken()
        {
            var refreshTokenRepository = new Mock<IRefreshTokenRepository>();

            refreshTokenRepository.Setup(v => v.TryGetRefreshTokenAsync("X")).ReturnsAsync(new RefreshToken { HashedId = "X" });

            var handler = new GetRefreshTokenQueryHandler(refreshTokenRepository.Object);

            var result = await handler.HandleAsync(new GetRefreshTokenQuery(new HashedRefreshTokenId("X")));

            refreshTokenRepository.Verify(v => v.TryGetRefreshTokenAsync("X"));

            Assert.AreEqual("X", result.HashedId);
        }
    }
}