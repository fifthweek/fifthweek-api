namespace Fifthweek.Api.Identity.Tests.OAuth
{
    using System;
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Text;
    using System.Threading.Tasks;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Membership.Commands;
    using Fifthweek.Api.Identity.Membership.Queries;
    using Fifthweek.Api.Identity.OAuth;
    using Fifthweek.Api.Identity.OAuth.Commands;
    using Fifthweek.Api.Identity.OAuth.Queries;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Shared;

    using Microsoft.Owin;
    using Microsoft.Owin.Security;
    using Microsoft.Owin.Security.OAuth;
    using Microsoft.Owin.Security.OAuth.Messages;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    using Constants = Fifthweek.Api.Core.Constants;

    [TestClass]
    public class FifthweekAuthorizationServerHandlerTests
    {
        private const string RequestHeaders = "owin.RequestHeaders";
        private const string ResponseHeaders = "owin.ResponseHeaders";

        private const string Username = "username";
        private const string Password = "password!";
        private const string ClientId = "clientId";
        private const string Secret = "secret!";
        private const string AuthenticationType = "bearer";
        private static readonly Client Client = new Client(
           new ClientId(ClientId),
           Helper.GetHash(Secret),
           "test",
           ApplicationType.JavaScript,
           false,
           100,
           "[a-z]+",
           "defaultAllowedOrigin");

        private static readonly UserClaimsIdentity UserClaimsIdentity = new UserClaimsIdentity(
            new UserId(Guid.NewGuid()),
            new Username(Username),
            new ClaimsIdentity(
                new Claim[] { new Claim(ClaimTypes.Role, "One"), new Claim(ClaimTypes.Role, "Two") },
                AuthenticationType));

        private FifthweekAuthorizationServerHandler target;
        private Mock<IQueryHandler<GetValidatedClientQuery, Client>> getValidatedClient;
        private Mock<IQueryHandler<GetUserClaimsIdentityQuery, UserClaimsIdentity>> getUserClaimsIdentity;
        private Mock<ICommandHandler<UpdateLastAccessTokenDateCommand>> updateLastAccessTokenDate;
        private Mock<IOwinExceptionHandler> exceptionHandler;

        private IDictionary<string, object> environment;
        private Dictionary<string, string[]> requestHeaders;
        private Dictionary<string, string[]> responseHeaders;

        [TestInitialize]
        public void TestInitialize()
        {
            this.environment = new Dictionary<string, object>(StringComparer.Ordinal);
            this.environment[RequestHeaders] = this.requestHeaders = new Dictionary<string, string[]>(StringComparer.OrdinalIgnoreCase);
            this.environment[ResponseHeaders] = this.responseHeaders = new Dictionary<string, string[]>(StringComparer.OrdinalIgnoreCase);

            this.getValidatedClient = new Mock<IQueryHandler<GetValidatedClientQuery, Client>>(MockBehavior.Strict);
            this.getUserClaimsIdentity = new Mock<IQueryHandler<GetUserClaimsIdentityQuery, UserClaimsIdentity>>(MockBehavior.Strict);
            this.updateLastAccessTokenDate = new Mock<ICommandHandler<UpdateLastAccessTokenDateCommand>>(MockBehavior.Strict);
            this.exceptionHandler = new Mock<IOwinExceptionHandler>();

            this.target = new FifthweekAuthorizationServerHandler(
                this.getValidatedClient.Object,
                this.getUserClaimsIdentity.Object,
                this.updateLastAccessTokenDate.Object,
                this.exceptionHandler.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenValidatingClientAuthentication_AndContextIsNull_ItShouldThrowAnException()
        {
            await this.target.ValidateClientAuthenticationAsync(null);
        }

        [TestMethod]
        public async Task WhenValidatingClientAuthentication_AndNoCredentialsAreProvided_ItShouldNotValidate()
        {
            var context = new OAuthValidateClientAuthenticationContext(
                new OwinContext(this.environment),
                new OAuthAuthorizationServerOptions(),
                new ReadableStringCollection(new Dictionary<string, string[]>()));

            await this.target.ValidateClientAuthenticationAsync(context);

            Assert.IsTrue(context.HasError);
            Assert.IsFalse(context.IsValidated);
        }

        [TestMethod]
        public async Task WhenValidatingClientAuthentication_AndBasicCredentialsAreValid_ItShouldValidate()
        {
            this.AddBasicAuthorization();

            var context = new OAuthValidateClientAuthenticationContext(
                new OwinContext(this.environment),
                new OAuthAuthorizationServerOptions(),
                new ReadableStringCollection(new Dictionary<string, string[]>()));

            this.getValidatedClient.Setup(v => v.HandleAsync(new GetValidatedClientQuery(Client.ClientId, Secret)))
                .ReturnsAsync(Client);

            await this.target.ValidateClientAuthenticationAsync(context);

            Assert.IsTrue(context.IsValidated);
        }

        [TestMethod]
        public async Task WhenValidatingClientAuthentication_AndFormCredentialsAreValid_ItShouldValidate()
        {
            var context = new OAuthValidateClientAuthenticationContext(
                new OwinContext(environment),
                new OAuthAuthorizationServerOptions(),
                new ReadableStringCollection(new Dictionary<string, string[]>
                {
                    { "client_id", new string[] { ClientId } },
                    { "client_secret", new string[] { Secret } }
                }));

            this.getValidatedClient.Setup(v => v.HandleAsync(new GetValidatedClientQuery(Client.ClientId, Secret)))
                .ReturnsAsync(Client);

            await this.target.ValidateClientAuthenticationAsync(context);

            Assert.IsTrue(context.IsValidated);
        }

        [TestMethod]
        public async Task WhenValidatingClientAuthentication_AndClientValidationHandlerThrowsClientRequestException_ItShouldNotValidate()
        {
            this.AddBasicAuthorization();

            var context = new OAuthValidateClientAuthenticationContext(
                new OwinContext(this.environment),
                new OAuthAuthorizationServerOptions(),
                new ReadableStringCollection(new Dictionary<string, string[]>()));

            this.getValidatedClient.Setup(v => v.HandleAsync(new GetValidatedClientQuery(Client.ClientId, Secret)))
                .ThrowsAsync(new ClientRequestException("Bad"));

            await this.target.ValidateClientAuthenticationAsync(context);

            Assert.IsTrue(context.HasError);
            Assert.IsFalse(context.IsValidated);
        }

        [TestMethod]
        [ExpectedException(typeof(DivideByZeroException))]
        public async Task WhenValidatingClientAuthentication_AndClientValidationHandlerThrowsUnexpectedException_ItShouldThrowAnException()
        {
            this.AddBasicAuthorization();

            var context = new OAuthValidateClientAuthenticationContext(
                new OwinContext(this.environment),
                new OAuthAuthorizationServerOptions(),
                new ReadableStringCollection(new Dictionary<string, string[]>()));

            this.getValidatedClient.Setup(v => v.HandleAsync(new GetValidatedClientQuery(Client.ClientId, Secret)))
                .ThrowsAsync(new DivideByZeroException());

            await this.target.ValidateClientAuthenticationAsync(context);
        }

        [TestMethod]
        public async Task WhenValidatingClientAuthentication_AndNoOriginHeaderPresent_ItShouldReportAndUseDefaultOrigin()
        {
            this.AddBasicAuthorization();

            var context = new OAuthValidateClientAuthenticationContext(
                new OwinContext(this.environment),
                new OAuthAuthorizationServerOptions(),
                new ReadableStringCollection(new Dictionary<string, string[]>()));

            this.getValidatedClient.Setup(v => v.HandleAsync(new GetValidatedClientQuery(Client.ClientId, Secret)))
                .ReturnsAsync(Client);

            Exception reportedException = null;
            this.exceptionHandler.Setup(v => v.ReportExceptionAsync(context.Request, It.IsAny<Exception>()))
                .Callback<IOwinRequest, Exception>((r, e) => reportedException = e);

            await this.target.ValidateClientAuthenticationAsync(context);

            Assert.IsNotNull(reportedException);
            Assert.AreEqual("Origin header not found.", reportedException.Message);

            ////Assert.AreEqual(Client.DefaultAllowedOrigin, context.Response.Headers[Constants.AllowedOriginHeaderKey]);
            Assert.AreEqual(Constants.DefaultAllowedOrigin, context.Response.Headers[Constants.AllowedOriginHeaderKey]);
        }

        [TestMethod]
        public async Task WhenValidatingClientAuthentication_AndOriginHeaderPresentAndExpected_ItShouldNotReportAndUseOrigin()
        {
            this.AddBasicAuthorization();

            this.requestHeaders.Add("Origin", new[] { "expected" });

            var context = new OAuthValidateClientAuthenticationContext(
                new OwinContext(this.environment),
                new OAuthAuthorizationServerOptions(),
                new ReadableStringCollection(new Dictionary<string, string[]>()));

            this.getValidatedClient.Setup(v => v.HandleAsync(new GetValidatedClientQuery(Client.ClientId, Secret)))
                .ReturnsAsync(Client);

            Exception reportedException = null;
            this.exceptionHandler.Setup(v => v.ReportExceptionAsync(context.Request, It.IsAny<Exception>()))
                .Callback<IOwinRequest, Exception>((r, e) => reportedException = e);

            await this.target.ValidateClientAuthenticationAsync(context);

            Assert.IsNull(reportedException);

            Assert.AreEqual("expected", context.Response.Headers[Constants.AllowedOriginHeaderKey]);
        }

        [TestMethod]
        public async Task WhenValidatingClientAuthentication_AndOriginHeaderPresentButUnexpected_ItShouldReportAndUseDefaultOrigin()
        {
            this.AddBasicAuthorization();

            this.requestHeaders.Add("Origin", new[] { "UNEXPECTED" });

            var context = new OAuthValidateClientAuthenticationContext(
                new OwinContext(this.environment),
                new OAuthAuthorizationServerOptions(),
                new ReadableStringCollection(new Dictionary<string, string[]>()));

            this.getValidatedClient.Setup(v => v.HandleAsync(new GetValidatedClientQuery(Client.ClientId, Secret)))
                .ReturnsAsync(Client);

            Exception reportedException = null;
            this.exceptionHandler.Setup(v => v.ReportExceptionAsync(context.Request, It.IsAny<Exception>()))
                .Callback<IOwinRequest, Exception>((r, e) => reportedException = e);

            await this.target.ValidateClientAuthenticationAsync(context);

            Assert.IsNotNull(reportedException);
            Assert.AreEqual("Unexpected origin: UNEXPECTED", reportedException.Message);

            ////Assert.AreEqual(Client.DefaultAllowedOrigin, context.Response.Headers[Constants.AllowedOriginHeaderKey]);
            Assert.AreEqual(Constants.DefaultAllowedOrigin, context.Response.Headers[Constants.AllowedOriginHeaderKey]);
        }

        [TestMethod]
        public async Task WhenValidatingClientAuthentication_AndItSucceeds_ItShouldStoreTheAllowedOriginAndRefreshTokenLifetimeInTheContext()
        {
            this.AddBasicAuthorization();

            this.requestHeaders.Add("Origin", new[] { "expected" });

            var context = new OAuthValidateClientAuthenticationContext(
                new OwinContext(this.environment),
                new OAuthAuthorizationServerOptions(),
                new ReadableStringCollection(new Dictionary<string, string[]>()));

            this.getValidatedClient.Setup(v => v.HandleAsync(new GetValidatedClientQuery(Client.ClientId, Secret)))
                .ReturnsAsync(Client);

            await this.target.ValidateClientAuthenticationAsync(context);

            Assert.AreEqual("expected", context.OwinContext.Get<string>(Constants.TokenAllowedOriginKey));
            Assert.AreEqual(Client.RefreshTokenLifeTimeMinutes, context.OwinContext.Get<int>(Constants.TokenRefreshTokenLifeTimeKey));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenGrantingResourceOwnerCredentials_AndContextIsNull_ItShouldThrowAnException()
        {
            await this.target.GrantResourceOwnerCredentialsAsync(null);
        }

        [TestMethod]
        [ExpectedException(typeof(UnauthorizedException))]
        public async Task WhenGrantingResourceOwnerCredentials_AndTokenAllowedOriginIsNotPopulated_ItShouldThrowAnException()
        {
            var context = new OAuthGrantResourceOwnerCredentialsContext(
                new OwinContext(this.environment),
                new OAuthAuthorizationServerOptions() { AuthenticationType = AuthenticationType },
                ClientId,
                Username,
                Password,
                new List<string>());

            await this.target.GrantResourceOwnerCredentialsAsync(context);
        }

        [TestMethod]
        public async Task WhenGrantingResourceOwnerCredentials_AndGetUserClaimsIdentityThrowsBadRequestException_ItShouldNotValidate()
        {
            var context = new OAuthGrantResourceOwnerCredentialsContext(
                new OwinContext(this.environment),
                new OAuthAuthorizationServerOptions() { AuthenticationType = AuthenticationType },
                ClientId,
                Username,
                Password,
                new List<string>());

            context.OwinContext.Set<string>(Constants.TokenAllowedOriginKey, Constants.DefaultAllowedOrigin);

            this.getUserClaimsIdentity
                .Setup(v => v.HandleAsync(new GetUserClaimsIdentityQuery(null, new Username(Username), new Password(Password), AuthenticationType)))
                .Throws(new BadRequestException("message"));

            await this.target.GrantResourceOwnerCredentialsAsync(context);

            Assert.IsTrue(context.HasError);
            Assert.AreEqual("message", context.ErrorDescription);
        }

        [TestMethod]
        public async Task WhenGrantingResourceOwnerCredentials_AndValidInformationIsGiven_ItShouldValidate()
        {
            var context = new OAuthGrantResourceOwnerCredentialsContext(
                new OwinContext(this.environment),
                new OAuthAuthorizationServerOptions() { AuthenticationType = AuthenticationType },
                ClientId,
                Username,
                Password,
                new List<string>());

            context.OwinContext.Set<string>(Constants.TokenAllowedOriginKey, Constants.DefaultAllowedOrigin);

            this.getUserClaimsIdentity
                .Setup(v => v.HandleAsync(new GetUserClaimsIdentityQuery(null, new Username(Username), new Password(Password), AuthenticationType)))
                .ReturnsAsync(UserClaimsIdentity);

            this.updateLastAccessTokenDate
                .Setup(v => v.HandleAsync(It.IsAny<UpdateLastAccessTokenDateCommand>()))
                .Returns(Task.FromResult(0));

            await this.target.GrantResourceOwnerCredentialsAsync(context);

            Assert.IsTrue(context.IsValidated);
            Assert.AreEqual(UserClaimsIdentity.ClaimsIdentity, context.Ticket.Identity);
        }

        [TestMethod]
        public async Task WhenGrantingResourceOwnerCredentials_AndValidInformationIsGiven_ItShouldSetTheTicketProperties()
        {
            var context = new OAuthGrantResourceOwnerCredentialsContext(
                new OwinContext(this.environment),
                new OAuthAuthorizationServerOptions() { AuthenticationType = AuthenticationType },
                ClientId,
                Username,
                Password,
                new List<string>());

            context.OwinContext.Set<string>(Constants.TokenAllowedOriginKey, Constants.DefaultAllowedOrigin);

            this.getUserClaimsIdentity
                .Setup(v => v.HandleAsync(new GetUserClaimsIdentityQuery(null, new Username(Username), new Password(Password), AuthenticationType)))
                .ReturnsAsync(UserClaimsIdentity);

            this.updateLastAccessTokenDate
                .Setup(v => v.HandleAsync(It.IsAny<UpdateLastAccessTokenDateCommand>()))
                .Returns(Task.FromResult(0));

            await this.target.GrantResourceOwnerCredentialsAsync(context);

            Assert.IsTrue(context.Ticket.Properties.Dictionary.ContainsKey(Constants.TokenClientIdKey));
            Assert.AreEqual(ClientId, context.Ticket.Properties.Dictionary[Constants.TokenClientIdKey]);

            Assert.IsTrue(context.Ticket.Properties.Dictionary.ContainsKey("username"));
            Assert.AreEqual(Username, context.Ticket.Properties.Dictionary["username"]);

            Assert.IsTrue(context.Ticket.Properties.Dictionary.ContainsKey("user_id"));
            Assert.AreEqual(UserClaimsIdentity.UserId.Value.EncodeGuid(), context.Ticket.Properties.Dictionary["user_id"]);

            Assert.IsTrue(context.Ticket.Properties.Dictionary.ContainsKey("roles"));
            Assert.AreEqual("One,Two", context.Ticket.Properties.Dictionary["roles"]);
        }

        [TestMethod]
        public async Task WhenGrantingResourceOwnerCredentials_AndValidInformationIsGiven_ItShouldUpdateTheAccessTokenTimestamp()
        {
            var context = new OAuthGrantResourceOwnerCredentialsContext(
                new OwinContext(this.environment),
                new OAuthAuthorizationServerOptions() { AuthenticationType = AuthenticationType },
                ClientId,
                Username,
                Password,
                new List<string>());

            context.OwinContext.Set<string>(Constants.TokenAllowedOriginKey, Constants.DefaultAllowedOrigin);

            this.getUserClaimsIdentity
                .Setup(v => v.HandleAsync(new GetUserClaimsIdentityQuery(null, new Username(Username), new Password(Password), AuthenticationType)))
                .ReturnsAsync(UserClaimsIdentity);

            UpdateLastAccessTokenDateCommand updateTimestampCommand = null;
            this.updateLastAccessTokenDate
                .Setup(v => v.HandleAsync(It.IsAny<UpdateLastAccessTokenDateCommand>()))
                .Callback<UpdateLastAccessTokenDateCommand>(v => updateTimestampCommand = v)
                .Returns(Task.FromResult(0));

            var before = DateTime.UtcNow;
            await this.target.GrantResourceOwnerCredentialsAsync(context);
            var after = DateTime.UtcNow;

            Assert.IsNotNull(updateTimestampCommand);
            Assert.AreEqual(UserClaimsIdentity.UserId, updateTimestampCommand.UserId);
            Assert.AreEqual(UpdateLastAccessTokenDateCommand.AccessTokenCreationType.SignIn, updateTimestampCommand.CreationType);
            Assert.IsTrue(updateTimestampCommand.Timestamp <= after);
            Assert.IsTrue(updateTimestampCommand.Timestamp >= before);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenTokenEndpointCalled_AndContextIsNull_ItShouldThrowAnException()
        {
            await this.target.TokenEndpointAsync(null);
        }

        [TestMethod]
        public async Task WhenTokenEndpointCalled_AndContextIsPopulated_ItShouldCopyAllPropertiesToTheAdditionalResponseParameters()
        {
            var context = new OAuthTokenEndpointContext(
                new OwinContext(this.environment),
                new OAuthAuthorizationServerOptions(),
                new AuthenticationTicket(new ClaimsIdentity(), new AuthenticationProperties(new Dictionary<string, string> { { "test1", "one" }, { "test2", "two" } })),
                new TokenEndpointRequest(new ReadableStringCollection(new Dictionary<string, string[]>())));

            await this.target.TokenEndpointAsync(context);

            Assert.AreEqual(2, context.AdditionalResponseParameters.Count);
            Assert.AreEqual("one", context.AdditionalResponseParameters["test1"]);
            Assert.AreEqual("two", context.AdditionalResponseParameters["test2"]);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenGrantingRefreshToken_AndContextIsNull_ItShouldThrowAnException()
        {
            await this.target.GrantRefreshTokenAsync(null);
        }

        [TestMethod]
        public async Task WhenGrantingRefreshToken_AndItSucceedes_ItValidateTheContext()
        {
            var context = new OAuthGrantRefreshTokenContext(
                new OwinContext(this.environment),
                new OAuthAuthorizationServerOptions() { AuthenticationType = AuthenticationType },
                new AuthenticationTicket(
                    new ClaimsIdentity(
                        new List<Claim> { new Claim(ClaimTypes.NameIdentifier, UserClaimsIdentity.UserId.Value.EncodeGuid()) },
                        AuthenticationType),
                    new AuthenticationProperties(new Dictionary<string, string> { { Constants.TokenClientIdKey, ClientId } })),
                ClientId);

            this.getUserClaimsIdentity
                .Setup(v => v.HandleAsync(new GetUserClaimsIdentityQuery(UserClaimsIdentity.UserId, null, null, AuthenticationType)))
                .ReturnsAsync(UserClaimsIdentity);

            this.updateLastAccessTokenDate
                .Setup(v => v.HandleAsync(It.IsAny<UpdateLastAccessTokenDateCommand>()))
                .Returns(Task.FromResult(0));

            await this.target.GrantRefreshTokenAsync(context);

            Assert.IsTrue(context.IsValidated);
            Assert.AreEqual(UserClaimsIdentity.ClaimsIdentity, context.Ticket.Identity);
        }

        [TestMethod]
        public async Task WhenGrantingRefreshToken_AndItSucceedes_ItSetTheTicketProperties()
        {
            var context = new OAuthGrantRefreshTokenContext(
                new OwinContext(this.environment),
                new OAuthAuthorizationServerOptions() { AuthenticationType = AuthenticationType },
                new AuthenticationTicket(
                    new ClaimsIdentity(
                        new List<Claim> { new Claim(ClaimTypes.NameIdentifier, UserClaimsIdentity.UserId.Value.EncodeGuid()) },
                        AuthenticationType),
                    new AuthenticationProperties(new Dictionary<string, string> { { Constants.TokenClientIdKey, ClientId } })),
                ClientId);

            this.getUserClaimsIdentity
                .Setup(v => v.HandleAsync(new GetUserClaimsIdentityQuery(UserClaimsIdentity.UserId, null, null, AuthenticationType)))
                .ReturnsAsync(UserClaimsIdentity);

            this.updateLastAccessTokenDate
                .Setup(v => v.HandleAsync(It.IsAny<UpdateLastAccessTokenDateCommand>()))
                .Returns(Task.FromResult(0));

            await this.target.GrantRefreshTokenAsync(context);

            Assert.IsTrue(context.Ticket.Properties.Dictionary.ContainsKey(Constants.TokenClientIdKey));
            Assert.AreEqual(ClientId, context.Ticket.Properties.Dictionary[Constants.TokenClientIdKey]);

            Assert.IsTrue(context.Ticket.Properties.Dictionary.ContainsKey("username"));
            Assert.AreEqual(Username, context.Ticket.Properties.Dictionary["username"]);

            Assert.IsTrue(context.Ticket.Properties.Dictionary.ContainsKey("user_id"));
            Assert.AreEqual(UserClaimsIdentity.UserId.Value.EncodeGuid(), context.Ticket.Properties.Dictionary["user_id"]);

            Assert.IsTrue(context.Ticket.Properties.Dictionary.ContainsKey("roles"));
            Assert.AreEqual("One,Two", context.Ticket.Properties.Dictionary["roles"]);
        }

        [TestMethod]
        public async Task WhenGrantingRefreshToken_AndItSucceedes_ItShouldUpdateTheAccessTokenTimestamp()
        {
            var context = new OAuthGrantRefreshTokenContext(
                new OwinContext(this.environment),
                new OAuthAuthorizationServerOptions() { AuthenticationType = AuthenticationType },
                new AuthenticationTicket(
                    new ClaimsIdentity(
                        new List<Claim> { new Claim(ClaimTypes.NameIdentifier, UserClaimsIdentity.UserId.Value.EncodeGuid()) },
                        AuthenticationType),
                    new AuthenticationProperties(new Dictionary<string, string> { { Constants.TokenClientIdKey, ClientId } })),
                ClientId);

            this.getUserClaimsIdentity
                .Setup(v => v.HandleAsync(new GetUserClaimsIdentityQuery(UserClaimsIdentity.UserId, null, null, AuthenticationType)))
                .ReturnsAsync(UserClaimsIdentity);

            UpdateLastAccessTokenDateCommand updateTimestampCommand = null;
            this.updateLastAccessTokenDate
                .Setup(v => v.HandleAsync(It.IsAny<UpdateLastAccessTokenDateCommand>()))
                .Callback<UpdateLastAccessTokenDateCommand>(v => updateTimestampCommand = v)
                .Returns(Task.FromResult(0));

            var before = DateTime.UtcNow;
            await this.target.GrantRefreshTokenAsync(context);
            var after = DateTime.UtcNow;

            Assert.IsNotNull(updateTimestampCommand);
            Assert.AreEqual(UserClaimsIdentity.UserId, updateTimestampCommand.UserId);
            Assert.AreEqual(UpdateLastAccessTokenDateCommand.AccessTokenCreationType.RefreshToken, updateTimestampCommand.CreationType);
            Assert.IsTrue(updateTimestampCommand.Timestamp <= after);
            Assert.IsTrue(updateTimestampCommand.Timestamp >= before);
        }

        [TestMethod]
        public async Task WhenGrantingRefreshToken_AndTheClientIdDoesNotMatch_ItShouldError()
        {
            var context = new OAuthGrantRefreshTokenContext(
                new OwinContext(this.environment),
                new OAuthAuthorizationServerOptions() { AuthenticationType = AuthenticationType },
                new AuthenticationTicket(
                    new ClaimsIdentity(
                        new List<Claim> { new Claim(ClaimTypes.NameIdentifier, UserClaimsIdentity.UserId.Value.EncodeGuid()) },
                        AuthenticationType),
                    new AuthenticationProperties(new Dictionary<string, string> { { Constants.TokenClientIdKey, ClientId } })),
                ClientId + "_Invalid");

            await this.target.GrantRefreshTokenAsync(context);

            Assert.IsFalse(context.IsValidated);
            Assert.IsTrue(context.HasError);
        }

        [TestMethod]
        public async Task WhenGrantingRefreshToken_AndGetUserClaimsIdentityThrowsBadRequestException_ItShouldError()
        {
            var context = new OAuthGrantRefreshTokenContext(
                new OwinContext(this.environment),
                new OAuthAuthorizationServerOptions() { AuthenticationType = AuthenticationType },
                new AuthenticationTicket(
                    new ClaimsIdentity(
                        new List<Claim> { new Claim(ClaimTypes.NameIdentifier, UserClaimsIdentity.UserId.Value.EncodeGuid()) },
                        AuthenticationType),
                    new AuthenticationProperties(new Dictionary<string, string> { { Constants.TokenClientIdKey, ClientId } })),
                ClientId);

            this.getUserClaimsIdentity
                .Setup(v => v.HandleAsync(new GetUserClaimsIdentityQuery(UserClaimsIdentity.UserId, null, null, AuthenticationType)))
                .ThrowsAsync(new BadRequestException("message"));

            await this.target.GrantRefreshTokenAsync(context);

            Assert.IsFalse(context.IsValidated);
            Assert.IsTrue(context.HasError);
            Assert.AreEqual("message", context.ErrorDescription);
        }

        private void AddBasicAuthorization()
        {
            var authorizationString = Convert.ToBase64String(Encoding.UTF8.GetBytes(ClientId + ":" + Secret));
            this.requestHeaders.Add("Authorization", new[] { "Basic " + authorizationString });
        }
    }
}