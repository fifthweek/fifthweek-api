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
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Shared;

    using Microsoft.Owin;
    using Microsoft.Owin.Security;
    using Microsoft.Owin.Security.DataHandler;
    using Microsoft.Owin.Security.Infrastructure;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    using Constants = Fifthweek.Api.Core.Constants;

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

        private static readonly DateTime Now = DateTime.UtcNow;

        private static readonly AuthenticationTicket Ticket = new AuthenticationTicket(
            new ClaimsIdentity(
                new Claim[] { new Claim(ClaimTypes.Name, Username) },
                AuthenticationType),
            new AuthenticationProperties(new Dictionary<string, string> { { Constants.TokenClientIdKey, ClientId } }));

        private static readonly RefreshToken RefreshToken = new RefreshToken { EncryptedId = "encryptedId", ProtectedTicket = ProtectedTicket };

        private IDictionary<string, object> environment;
        private Dictionary<string, string[]> requestHeaders;
        private Dictionary<string, string[]> responseHeaders;
        
        private Mock<ICommandHandler<SetRefreshTokenCommand>> createRefreshToken;
        private Mock<IQueryHandler<TryGetRefreshTokenQuery, RefreshToken>> tryGetRefreshToken;
        private Mock<IQueryHandler<TryGetRefreshTokenByEncryptedIdQuery, RefreshToken>> tryGetRefreshTokenByEncryptedId;
        private Mock<IRefreshTokenIdEncryptionService> encryptionService;
        private Mock<ITimestampCreator> timestampCreator;

        private Mock<ISecureDataFormat<AuthenticationTicket>> secureDataFormat;

        private FifthweekRefreshTokenHandler target;

        public virtual void TestInitialize()
        {
            this.environment = new Dictionary<string, object>(StringComparer.Ordinal);
            this.environment[RequestHeaders] = this.requestHeaders = new Dictionary<string, string[]>(StringComparer.OrdinalIgnoreCase);
            this.environment[ResponseHeaders] = this.responseHeaders = new Dictionary<string, string[]>(StringComparer.OrdinalIgnoreCase);
            
            this.createRefreshToken = new Mock<ICommandHandler<SetRefreshTokenCommand>>(MockBehavior.Strict);
            this.tryGetRefreshToken = new Mock<IQueryHandler<TryGetRefreshTokenQuery, RefreshToken>>(MockBehavior.Strict);
            this.tryGetRefreshTokenByEncryptedId = new Mock<IQueryHandler<TryGetRefreshTokenByEncryptedIdQuery, RefreshToken>>(MockBehavior.Strict);
            this.encryptionService = new Mock<IRefreshTokenIdEncryptionService>(MockBehavior.Strict);
            this.timestampCreator = new Mock<ITimestampCreator>();

            this.timestampCreator.Setup(v => v.Now()).Returns(Now);

            this.secureDataFormat = new Mock<ISecureDataFormat<AuthenticationTicket>>();
            this.secureDataFormat.Setup(v => v.Protect(Ticket)).Returns(ProtectedTicket);
            this.secureDataFormat.Setup(v => v.Unprotect(ProtectedTicket)).Returns(Ticket);

            this.target = new FifthweekRefreshTokenHandler(
                this.createRefreshToken.Object,
                this.tryGetRefreshToken.Object,
                this.tryGetRefreshTokenByEncryptedId.Object,
                this.encryptionService.Object,
                this.timestampCreator.Object);
        }

        [TestClass]
        public class FifthweekRefreshTokenHandlerTests_CreateAsync : FifthweekRefreshTokenHandlerTests
        {
            [TestInitialize]
            public override void TestInitialize()
            {
                base.TestInitialize();
            }

            [TestMethod]
            [ExpectedException(typeof(ArgumentNullException))]
            public async Task WhenContextIsNull_ItShouldThrowAnException()
            {
                await this.target.CreateAsync(null);
            }

            [TestMethod]
            public async Task WhenContextIsValid_AndRefreshTokenNotFound_ItShouldCreateARefreshToken()
            {
                var context = new AuthenticationTokenCreateContext(
                    new OwinContext(this.environment),
                    this.secureDataFormat.Object,
                    Ticket);

                context.OwinContext.Set<int>(Constants.TokenRefreshTokenLifeTimeKey, RefreshTokenLifetimeMinutes);

                this.tryGetRefreshToken.Setup(v => v.HandleAsync(new TryGetRefreshTokenQuery(new ClientId(ClientId), new Username(Username))))
                    .ReturnsAsync(null);

                SetRefreshTokenCommand command = null;
                this.createRefreshToken.Setup(v => v.HandleAsync(It.IsAny<SetRefreshTokenCommand>()))
                    .Callback<SetRefreshTokenCommand>(v => command = v)
                    .Returns(Task.FromResult(0));

                await this.target.CreateAsync(context);

                Assert.IsNotNull(command);
                Assert.AreEqual(ClientId, command.ClientId.Value);
                Assert.AreEqual(Username, command.Username.Value);
                Assert.AreEqual(ProtectedTicket, command.ProtectedTicket);
                Assert.AreEqual(Now, command.IssuedDate);
                Assert.AreEqual(Now.AddMinutes(RefreshTokenLifetimeMinutes), command.ExpiresDate);
                Assert.IsNotNull(command.RefreshTokenId);

                Assert.AreEqual(context.Token, command.RefreshTokenId.Value);
            }

            [TestMethod]
            public async Task WhenContextIsValid_AndRefreshTokenFoundAndExpired_ItShouldCreateARefreshToken()
            {
                var context = new AuthenticationTokenCreateContext(
                    new OwinContext(this.environment),
                    this.secureDataFormat.Object,
                    Ticket);

                context.OwinContext.Set<int>(Constants.TokenRefreshTokenLifeTimeKey, RefreshTokenLifetimeMinutes);

                this.tryGetRefreshToken.Setup(v => v.HandleAsync(new TryGetRefreshTokenQuery(new ClientId(ClientId), new Username(Username))))
                    .ReturnsAsync(new RefreshToken { ExpiresDate = Now });

                SetRefreshTokenCommand command = null;
                this.createRefreshToken.Setup(v => v.HandleAsync(It.IsAny<SetRefreshTokenCommand>()))
                    .Callback<SetRefreshTokenCommand>(v => command = v)
                    .Returns(Task.FromResult(0));

                await this.target.CreateAsync(context);

                Assert.IsNotNull(command);
                Assert.AreEqual(ClientId, command.ClientId.Value);
                Assert.AreEqual(Username, command.Username.Value);
                Assert.AreEqual(ProtectedTicket, command.ProtectedTicket);
                Assert.AreEqual(Now, command.IssuedDate);
                Assert.AreEqual(Now.AddMinutes(RefreshTokenLifetimeMinutes), command.ExpiresDate);
                Assert.IsNotNull(command.RefreshTokenId);

                Assert.AreEqual(context.Token, command.RefreshTokenId.Value);
            }

            [TestMethod]
            public async Task WhenContextIsValid_AndRefreshTokenFoundAndNotExpired_ItShouldCreateARefreshToken()
            {
                var context = new AuthenticationTokenCreateContext(
                    new OwinContext(this.environment),
                    this.secureDataFormat.Object,
                    Ticket);

                context.OwinContext.Set<int>(Constants.TokenRefreshTokenLifeTimeKey, RefreshTokenLifetimeMinutes);

                this.tryGetRefreshToken.Setup(v => v.HandleAsync(new TryGetRefreshTokenQuery(new ClientId(ClientId), new Username(Username))))
                    .ReturnsAsync(new RefreshToken { ExpiresDate = Now.AddTicks(1), EncryptedId = "encryptedId" });

                this.encryptionService.Setup(v => v.DecryptRefreshTokenId(new EncryptedRefreshTokenId("encryptedId"))).Returns(new RefreshTokenId("existingId"));

                SetRefreshTokenCommand command = null;
                this.createRefreshToken.Setup(v => v.HandleAsync(It.IsAny<SetRefreshTokenCommand>()))
                    .Callback<SetRefreshTokenCommand>(v => command = v)
                    .Returns(Task.FromResult(0));

                await this.target.CreateAsync(context);

                Assert.IsNull(command);

                Assert.AreEqual(context.Token, "existingId");
            }

            [TestMethod]
            [ExpectedException(typeof(InvalidOperationException))]
            public async Task WhenContextIsDoesNotContainRefreshTokenLifetime_ItShouldThrowAnException()
            {
                var context = new AuthenticationTokenCreateContext(
                    new OwinContext(this.environment),
                    this.secureDataFormat.Object,
                    Ticket);

                await this.target.CreateAsync(context);
            }
        }

        [TestClass]
        public class FifthweekRefreshTokenHandlerTests_ReceiveAsync : FifthweekRefreshTokenHandlerTests
        {
            [TestInitialize]
            public override void TestInitialize()
            {
                base.TestInitialize();
            }
            
            [TestMethod]
            [ExpectedException(typeof(ArgumentNullException))]
            public async Task WhenContextIsNull_ItShouldThrowAnException()
            {
                await this.target.ReceiveAsync(null);
            }

            [TestMethod]
            public async Task WhenContextIsValid_ItShouldSetTheProtectedTicket()
            {
                var context = new AuthenticationTokenReceiveContext(
                    new OwinContext(this.environment),
                    this.secureDataFormat.Object,
                    ProtectedTicket);

                context.OwinContext.Set<string>(Constants.TokenAllowedOriginKey, "origin");

                this.encryptionService.Setup(v => v.EncryptRefreshTokenId(new RefreshTokenId(ProtectedTicket)))
                    .Returns(new EncryptedRefreshTokenId(RefreshToken.EncryptedId));

                this.tryGetRefreshTokenByEncryptedId.Setup(v => v.HandleAsync(new TryGetRefreshTokenByEncryptedIdQuery(new EncryptedRefreshTokenId(RefreshToken.EncryptedId))))
                    .ReturnsAsync(RefreshToken);

                await this.target.ReceiveAsync(context);

                Assert.AreEqual(Ticket, context.Ticket);
            }

            [TestMethod]
            public async Task WhenContextIsValid_ButRefreshTokenIdDoesNotMatch_ItShouldNotSetTheProtectedTicket()
            {
                var context = new AuthenticationTokenReceiveContext(
                    new OwinContext(this.environment),
                    this.secureDataFormat.Object,
                    ProtectedTicket);

                context.OwinContext.Set<string>(Constants.TokenAllowedOriginKey, "origin");

                this.encryptionService.Setup(v => v.EncryptRefreshTokenId(new RefreshTokenId(ProtectedTicket)))
                    .Returns(new EncryptedRefreshTokenId("abc"));

                this.tryGetRefreshTokenByEncryptedId.Setup(v => v.HandleAsync(new TryGetRefreshTokenByEncryptedIdQuery(new EncryptedRefreshTokenId("abc"))))
                    .ReturnsAsync(null);

                await this.target.ReceiveAsync(context);

                Assert.AreEqual(null, context.Ticket);
            }

            [TestMethod]
            [ExpectedException(typeof(InvalidOperationException))]
            public async Task WhenAllowedOriginIsNotSet_ItShouldThrowAnException()
            {
                var context = new AuthenticationTokenReceiveContext(
                    new OwinContext(this.environment),
                    this.secureDataFormat.Object,
                    ProtectedTicket);

                await this.target.ReceiveAsync(context);
            }
        }
    }
}