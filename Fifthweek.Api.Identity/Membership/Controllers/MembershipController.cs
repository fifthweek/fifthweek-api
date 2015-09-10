namespace Fifthweek.Api.Identity.Membership.Controllers
{
    using System;
    using System.Threading.Tasks;
    using System.Web.Http;
    using System.Web.Http.Description;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Membership.Commands;
    using Fifthweek.Api.Identity.Membership.Queries;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;

    [RoutePrefix("membership"), AutoConstructor]
    public partial class MembershipController : ApiController
    {
        private readonly IRequesterContext requesterContext;
        private readonly ICommandHandler<RegisterUserCommand> registerUser;
        private readonly ICommandHandler<RequestPasswordResetCommand> requestPasswordReset;
        private readonly ICommandHandler<ConfirmPasswordResetCommand> confirmPasswordReset;
        private readonly ICommandHandler<RegisterInterestCommand> registerInterest;
        private readonly ICommandHandler<SendIdentifiedUserInformationCommand> sendIdentifiedUserInformation;
        private readonly IQueryHandler<IsUsernameAvailableQuery, bool> isUsernameAvailable;
        private readonly IQueryHandler<IsPasswordResetTokenValidQuery, bool> isPasswordResetTokenValid;
        private readonly IGuidCreator guidCreator;

        // POST membership/registrations
        [AllowAnonymous]
        [Route("registrations")]
        public async Task<IHttpActionResult> PostRegistrationAsync(RegistrationData registrationData)
        {
            registrationData.AssertBodyProvided("registrationData");
            var registration = registrationData.Parse();

            var command = new RegisterUserCommand(
                new UserId(this.guidCreator.CreateSqlSequential()),
                registration.ExampleWork,
                registration.Email,
                registration.Username,
                registration.Password,
                registration.RegisterAsCreator);

            await this.registerUser.HandleAsync(command);
            return this.Ok();
        }

        // GET membership/availableUsernames
        [AllowAnonymous]
        [Route("availableUsernames/{username}")]
        public async Task<IHttpActionResult> GetUsernameAvailabilityAsync(string username)
        {
            username.AssertUrlParameterProvided("username");

            ValidUsername usernameObject;
            if (!ValidUsername.TryParse(username, out usernameObject))
            {
                return this.NotFound();
            }

            var query = new IsUsernameAvailableQuery(usernameObject);
            var usernameAvailable = await this.isUsernameAvailable.HandleAsync(query);
            if (usernameAvailable)
            {
                return this.Ok();
            }

            return this.NotFound();
        }

        // POST membership/passwordResetRequests
        [AllowAnonymous]
        [Route("passwordResetRequests")]
        public async Task<IHttpActionResult> PostPasswordResetRequestAsync(PasswordResetRequestData passwordResetRequestData)
        {
            passwordResetRequestData.AssertBodyProvided("passwordResetRequestData");
            var passwordResetRequest = passwordResetRequestData.Parse();

            var command = new RequestPasswordResetCommand(
                passwordResetRequest.Email,
                passwordResetRequest.Username);

            await this.requestPasswordReset.HandleAsync(command);

            return this.Ok();
        }

        // POST membership/passwordResetConfirmations
        [AllowAnonymous]
        [Route("passwordResetConfirmations")]
        public async Task<IHttpActionResult> PostPasswordResetConfirmationAsync(PasswordResetConfirmationData passwordResetConfirmationData)
        {
            passwordResetConfirmationData.AssertBodyProvided("passwordResetConfirmationData");
            var passwordResetConfirmation = passwordResetConfirmationData.Parse();

            var command = new ConfirmPasswordResetCommand(
                passwordResetConfirmation.UserId,
                passwordResetConfirmation.Token,
                passwordResetConfirmation.NewPassword);

            await this.confirmPasswordReset.HandleAsync(command);

            return this.Ok();
        }

        // GET membership/passwordResetTokens/{userId}?token={token}
        [AllowAnonymous]
        [Route("passwordResetTokens/{userId}")]
        public async Task<IHttpActionResult> GetPasswordResetTokenValidityAsync(string userId, [FromUri]string token)
        {
            userId.AssertUrlParameterProvided("userId");
            token.AssertUrlParameterProvided("token");

            var userIdObject = new UserId(userId.DecodeGuid());
            var query = new IsPasswordResetTokenValidQuery(userIdObject, token);
            var tokenValid = await this.isPasswordResetTokenValid.HandleAsync(query);
            if (tokenValid)
            {
                return this.Ok();
            }

            return this.NotFound();
        }

        // POST membership/registeredInterest
        [AllowAnonymous]
        [Route("registeredInterest")]
        public async Task<IHttpActionResult> PostRegisteredInterestAsync(RegisterInterestData registerInterestData)
        {
            registerInterestData.AssertBodyProvided("registerInterestData");
            var data = registerInterestData.Parse();

            var command = new RegisterInterestCommand(
                data.Name,
                data.Email);

            await this.registerInterest.HandleAsync(command);
            return this.Ok();
        }

        // POST membership/identifiedUsers
        [AllowAnonymous]
        [Route("identifiedUsers")]
        public async Task<IHttpActionResult> PostIdentifiedUserAsync(IdentifiedUserData identifiedUserData)
        {
            identifiedUserData.AssertBodyProvided("identifiedUserData");

            if (string.IsNullOrWhiteSpace(identifiedUserData.Email))
            {
                throw new BadRequestException("Email must be provided when identifying user");
            }

            var requester = await this.requesterContext.GetRequesterAsync();

            var command = new SendIdentifiedUserInformationCommand(
                requester,
                identifiedUserData.IsUpdate,
                new Email(identifiedUserData.Email),
                string.IsNullOrWhiteSpace(identifiedUserData.Name) ? null : identifiedUserData.Name,
                string.IsNullOrWhiteSpace(identifiedUserData.Username) ? null : new Username(identifiedUserData.Username));

            await this.sendIdentifiedUserInformation.HandleAsync(command);
            return this.Ok();
        }
    }
}
