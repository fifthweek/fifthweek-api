namespace Fifthweek.Api.Identity.Tests.OAuth
{
    using System;
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.OAuth;
    using Fifthweek.Api.Identity.OAuth.Commands;
    using Fifthweek.Api.Identity.OAuth.Queries;
    using Fifthweek.Api.Persistence;

    using Microsoft.Owin;
    using Microsoft.Owin.Security;
    using Microsoft.Owin.Security.DataHandler;
    using Microsoft.Owin.Security.Infrastructure;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class FifthweekRefreshTokenHandlerTests
    {
        private const string RequestHeaders = "owin.RequestHeaders";
        private const string ResponseHeaders = "owin.ResponseHeaders";

        private const string Username = "username";
        private const string Password = "password!";
        private const string ClientId = "clientId";

        private const string ProtectedTicket = "protected";
        private const string AuthenticationType = "bearer";

        private const int RefreshTokenLifetimeMinutes = 100;

        private static readonly AuthenticationTicket Ticket = new AuthenticationTicket(
            new ClaimsIdentity(
                new Claim[] { new Claim(ClaimTypes.Name, Username) },
                AuthenticationType),
            new AuthenticationProperties(new Dictionary<string, string> { { Constants.TokenClientIdKey, ClientId } }));

        private static readonly RefreshToken RefreshToken = new RefreshToken { ProtectedTicket = ProtectedTicket };

        private IDictionary<string, object> environment;
        private Dictionary<string, string[]> requestHeaders;
        private Dictionary<string, string[]> responseHeaders;
        
        private Mock<ICommandHandler<CreateRefreshTokenCommand>> createRefreshToken;
        private Mock<ICommandHandler<RemoveRefreshTokenCommand>> removeRefreshToken;
        private Mock<IQueryHandler<TryGetRefreshTokenQuery, RefreshToken>> tryGetRefreshToken;

        private Mock<ISecureDataFormat<AuthenticationTicket>> secureDataFormat;

        private FifthweekRefreshTokenHandler target;

        [TestInitialize]
        public void TestInitialize()
        {
            this.environment = new Dictionary<string, object>(StringComparer.Ordinal);
            this.environment[RequestHeaders] = this.requestHeaders = new Dictionary<string, string[]>(StringComparer.OrdinalIgnoreCase);
            this.environment[ResponseHeaders] = this.responseHeaders = new Dictionary<string, string[]>(StringComparer.OrdinalIgnoreCase);
            
            this.createRefreshToken = new Mock<ICommandHandler<CreateRefreshTokenCommand>>(MockBehavior.Strict);
            this.removeRefreshToken = new Mock<ICommandHandler<RemoveRefreshTokenCommand>>(MockBehavior.Strict);
            this.tryGetRefreshToken = new Mock<IQueryHandler<TryGetRefreshTokenQuery, RefreshToken>>(MockBehavior.Strict);

            this.secureDataFormat = new Mock<ISecureDataFormat<AuthenticationTicket>>();
            this.secureDataFormat.Setup(v => v.Protect(Ticket)).Returns(ProtectedTicket);
            this.secureDataFormat.Setup(v => v.Unprotect(ProtectedTicket)).Returns(Ticket);

            this.target = new FifthweekRefreshTokenHandler(
                this.createRefreshToken.Object,
                this.removeRefreshToken.Object,
                this.tryGetRefreshToken.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenCreating_AndContextIsNull_ItShouldThrowAnException()
        {
            await this.target.CreateAsync(null);
        }

        [TestMethod]
        public async Task WhenCreating_AndContextIsValid_ItShouldCreateARefreshToken()
        {
            var context = new AuthenticationTokenCreateContext(
                new OwinContext(this.environment),
                this.secureDataFormat.Object,
                Ticket);

            context.OwinContext.Set<int>(Constants.TokenRefreshTokenLifeTimeKey, RefreshTokenLifetimeMinutes);

            CreateRefreshTokenCommand command = null;
            this.createRefreshToken.Setup(v => v.HandleAsync(It.IsAny<CreateRefreshTokenCommand>()))
                .Callback<CreateRefreshTokenCommand>(v => command = v)
                .Returns(Task.FromResult(0));

            var before = DateTime.UtcNow;
            await this.target.CreateAsync(context);
            var after = DateTime.UtcNow;

            Assert.IsNotNull(command);
            Assert.AreEqual(ClientId, command.ClientId.Value);
            Assert.AreEqual(Username, command.Username.Value);
            Assert.AreEqual(ProtectedTicket, command.ProtectedTicket);
            Assert.IsTrue(command.IssuedDate >= before);
            Assert.IsTrue(command.IssuedDate <= after);
            Assert.AreEqual(command.ExpiresDate, command.IssuedDate.AddMinutes(RefreshTokenLifetimeMinutes));
            Assert.IsNotNull(command.RefreshTokenId);

            Assert.AreEqual(context.Token, command.RefreshTokenId.Value);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public async Task WhenCreating_AndContextIsDoesNotContainRefreshTokenLifetime_ItShouldThrowAnException()
        {
            var context = new AuthenticationTokenCreateContext(
                new OwinContext(this.environment),
                this.secureDataFormat.Object,
                Ticket);

            await this.target.CreateAsync(context);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenReceiving_AndContextIsNull_ItShouldThrowAnException()
        {
            await this.target.ReceiveAsync(null);
        }

        [TestMethod]
        public async Task WhenReceiving_AndContextIsValid_ItShouldSetTheProtectedTicket()
        {
            var context = new AuthenticationTokenReceiveContext(
                new OwinContext(this.environment),
                this.secureDataFormat.Object,
                ProtectedTicket);

            context.OwinContext.Set<string>(Constants.TokenAllowedOriginKey, "origin");

            var hashedToken = HashedRefreshTokenId.FromRefreshToken(ProtectedTicket);

            this.tryGetRefreshToken.Setup(v => v.HandleAsync(new TryGetRefreshTokenQuery(hashedToken)))
                .ReturnsAsync(RefreshToken);

            this.removeRefreshToken.Setup(v => v.HandleAsync(new RemoveRefreshTokenCommand(hashedToken)))
                .Returns(Task.FromResult(0))
                .Verifiable();

            await this.target.ReceiveAsync(context);

            this.removeRefreshToken.Verify();

            Assert.AreEqual(Ticket, context.Ticket);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public async Task WhenReceiving_AndAllowedOriginIsNotSet_ItShouldThrowAnException()
        {
            var context = new AuthenticationTokenReceiveContext(
                new OwinContext(this.environment),
                this.secureDataFormat.Object,
                ProtectedTicket);

            await this.target.ReceiveAsync(context);
        }

        [TestMethod]
        public async Task WhenReceiving_AndRefreshTokenIsNotFound_ItShouldNotSetTheProtectedTicket()
        {
            var context = new AuthenticationTokenReceiveContext(
                new OwinContext(this.environment),
                this.secureDataFormat.Object,
                ProtectedTicket);

            context.OwinContext.Set<string>(Constants.TokenAllowedOriginKey, "origin");

            var hashedToken = HashedRefreshTokenId.FromRefreshToken(ProtectedTicket);

            this.tryGetRefreshToken.Setup(v => v.HandleAsync(new TryGetRefreshTokenQuery(hashedToken)))
                .ReturnsAsync(null);

            await this.target.ReceiveAsync(context);

            Assert.AreEqual(null, context.Ticket);
        }
    }
}