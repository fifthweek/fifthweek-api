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

    [RoutePrefix("membership"), AutoConstructor]
    public partial class MembershipController : ApiController
    {
        private readonly ICommandHandler<RegisterUserCommand> registerUser;
        private readonly ICommandHandler<RequestPasswordResetCommand> requestPasswordReset;
        private readonly ICommandHandler<ConfirmPasswordResetCommand> confirmPasswordReset;
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
                registration.Password);

            await this.registerUser.HandleAsync(command);
            return this.Ok();
        }

        // GET membership/availableUsernames
        [AllowAnonymous]
        [Route("availableUsernames/{username}")]
        [ResponseType(typeof(bool))]
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

        // GET membership/passwordResetTokens/{userId}/{token}
        [AllowAnonymous]
        [Route("passwordResetTokens/{userId}/{*token}")]
        [ResponseType(typeof(bool))]
        public async Task<IHttpActionResult> GetPasswordResetTokenValidityAsync(string userId, string token)
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
    }
}
