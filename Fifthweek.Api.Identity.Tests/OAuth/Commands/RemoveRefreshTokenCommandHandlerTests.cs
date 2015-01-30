namespace Fifthweek.Api.Identity.Tests.OAuth.Commands
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Identity.OAuth;
    using Fifthweek.Api.Identity.OAuth.Commands;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class RemoveRefreshTokenCommandHandlerTests
    {
        private static readonly HashedRefreshTokenId HashedRefreshTokenId = new HashedRefreshTokenId("h");

        private RemoveRefreshTokenCommandHandler target;
        private Mock<IRemoveRefreshTokenDbStatement> removeRefreshTokenDbStatement;

        [TestInitialize]
        public void TestInitialize()
        {
            this.removeRefreshTokenDbStatement = new Mock<IRemoveRefreshTokenDbStatement>(MockBehavior.Strict);
            this.target = new RemoveRefreshTokenCommandHandler(this.removeRefreshTokenDbStatement.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenQueryIsNull_ItShouldThrowAnException()
        {
            await this.target.HandleAsync(null);
        }

        [TestMethod]
        public async Task WhenQueryIsValid_ItShouldReturnTheRefreshToken()
        {
            this.removeRefreshTokenDbStatement.Setup(v => v.ExecuteAsync(HashedRefreshTokenId)).Returns(Task.FromResult(0)).Verifiable();

            await this.target.HandleAsync(new RemoveRefreshTokenCommand(HashedRefreshTokenId));

            this.removeRefreshTokenDbStatement.Verify();
        }
    }
}