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
        public async Task<IHttpActionResult> PostRegistrationAsync(RegistrationData registration)
        {
            registration.Parse();

            var command = new RegisterUserCommand(
                new UserId(this.guidCreator.CreateSqlSequential()),
                registration.ExampleWork,
                registration.EmailObject,
                registration.UsernameObject,
                registration.PasswordObject);

            await this.registerUser.HandleAsync(command);
            return this.Ok();
        }

        // GET membership/availableUsernames
        [AllowAnonymous]
        [Route("availableUsernames/{username}")]
        [ResponseType(typeof(bool))]
        public async Task<IHttpActionResult> GetUsernameAvailabilityAsync(string username)
        {
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
        public async Task<IHttpActionResult> PostPasswordResetRequestAsync(PasswordResetRequestData passwordResetRequest)
        {
            passwordResetRequest.Parse();

            var command = new RequestPasswordResetCommand(
                passwordResetRequest.EmailObject,
                passwordResetRequest.UsernameObject);

            await this.requestPasswordReset.HandleAsync(command);

            return this.Ok();
        }

        // POST membership/passwordResetConfirmations
        [AllowAnonymous]
        [Route("passwordResetConfirmations")]
        public async Task<IHttpActionResult> PostPasswordResetConfirmationAsync(PasswordResetConfirmationData passwordResetConfirmation)
        {
            passwordResetConfirmation.Parse();

            var command = new ConfirmPasswordResetCommand(
                passwordResetConfirmation.UserId,
                passwordResetConfirmation.Token,
                passwordResetConfirmation.NewPasswordObject);

            await this.confirmPasswordReset.HandleAsync(command);

            return this.Ok();
        }

        // GET membership/passwordResetTokens/{userId}/{token}
        [AllowAnonymous]
        [Route("passwordResetTokens/{userId}/{*token}")]
        [ResponseType(typeof(bool))]
        public async Task<IHttpActionResult> GetPasswordResetTokenValidityAsync(Guid userId, string token)
        {
            var userIdObject = new UserId(userId);
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
