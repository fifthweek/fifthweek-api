namespace Fifthweek.Api.Tests.QueryHandlers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Fifthweek.Api.Entities;
    using Fifthweek.Api.Queries;
    using Fifthweek.Api.QueryHandlers;
    using Fifthweek.Api.Repositories;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class GetAllRefreshTokensQueryHandlerTests
    {
        [TestMethod]
        public async Task ItShouldReturnAllRefreshTokens()
        {
            var refreshTokenRepository = new Mock<IRefreshTokenRepository>();

            var expectedResult = new List<RefreshToken>
            {
                new RefreshToken { ClientId = "1" },
                new RefreshToken { ClientId = "2" }
            };

            refreshTokenRepository.Setup(v => v.GetAllRefreshTokensAsync())
                .ReturnsAsync(expectedResult);

            var handler = new GetAllRefreshTokensQueryHandler(refreshTokenRepository.Object);

            var result = await handler.HandleAsync(new GetAllRefreshTokensQuery());

            refreshTokenRepository.Verify(v => v.GetAllRefreshTokensAsync());

            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);
            Assert.AreSame(expectedResult.First(), result.First());
            Assert.AreSame(expectedResult.Last(), result.Last());
        }
    }
}