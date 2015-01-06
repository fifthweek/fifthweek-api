using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.ModelBinding;

namespace Fifthweek.Api.Identity.Membership.Controllers
{
    using System;
    using System.Threading.Tasks;
    using System.Web.Http;
    using System.Web.Http.Description;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Membership.Commands;
    using Fifthweek.Api.Identity.Membership.Queries;

    [RoutePrefix("membership")]
    public class MembershipController : ApiController
    {
        private readonly ICommandHandler<RegisterUserCommand> registerUser;
        private readonly ICommandHandler<RequestPasswordResetCommand> requestPasswordReset;
        private readonly ICommandHandler<ConfirmPasswordResetCommand> confirmPasswordReset;
        private readonly IQueryHandler<IsUsernameAvailableQuery, bool> isUsernameAvailable;
        private readonly IQueryHandler<IsPasswordResetTokenValidQuery, bool> isPasswordResetTokenValid;
        private readonly IGuidCreator guidCreator;

        public MembershipController(
            ICommandHandler<RegisterUserCommand> registerUser,
            ICommandHandler<RequestPasswordResetCommand> requestPasswordReset,
            ICommandHandler<ConfirmPasswordResetCommand> confirmPasswordReset,
            IQueryHandler<IsUsernameAvailableQuery, bool> isUsernameAvailable,
            IQueryHandler<IsPasswordResetTokenValidQuery, bool> isPasswordResetTokenValid,
            IGuidCreator guidCreator)
        {
            if (registerUser == null)
            {
                throw new ArgumentNullException("registerUser");
            }

            if (requestPasswordReset == null)
            {
                throw new ArgumentNullException("requestPasswordReset");
            }

            if (confirmPasswordReset == null)
            {
                throw new ArgumentNullException("confirmPasswordReset");
            }

            if (isUsernameAvailable == null)
            {
                throw new ArgumentNullException("isUsernameAvailable");
            }

            if (isPasswordResetTokenValid == null)
            {
                throw new ArgumentNullException("isPasswordResetTokenValid");
            }

            if (guidCreator == null)
            {
                throw new ArgumentNullException("guidCreator");
            }

            this.registerUser = registerUser;
            this.requestPasswordReset = requestPasswordReset;
            this.confirmPasswordReset = confirmPasswordReset;
            this.isUsernameAvailable = isUsernameAvailable;
            this.isPasswordResetTokenValid = isPasswordResetTokenValid;
            this.guidCreator = guidCreator;
        }

        // POST membership/registrations
        [AllowAnonymous]
        [Route("registrations")]
        public async Task<IHttpActionResult> PostRegistrationAsync(RegistrationData registration)
        {
            registration.Parse();

            var command = new RegisterUserCommand(
                UserId.Parse(this.guidCreator.CreateSqlSequential()),
                registration.ExampleWork,
                NormalizedEmail.Normalize(registration.EmailObj),
                NormalizedUsername.Normalize(registration.UsernameObj),
                registration.PasswordObj);

            await this.registerUser.HandleAsync(command);
            return this.Ok();
        }

        // GET membership/availableUsernames
        [AllowAnonymous]
        [Route("availableUsernames/{username}")]
        [ResponseType(typeof(bool))]
        public async Task<IHttpActionResult> GetUsernameAvailabilityAsync(string username)
        {
            Username usernameObj;
            if (!Username.TryParse(username, out usernameObj))
            {
                return this.NotFound();
            }

            var query = new IsUsernameAvailableQuery(NormalizedUsername.Normalize(usernameObj));
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
                passwordResetRequest.EmailObj == null ? null : NormalizedEmail.Normalize(passwordResetRequest.EmailObj),
                passwordResetRequest.UsernameObj == null ? null : NormalizedUsername.Normalize(passwordResetRequest.UsernameObj)
            );

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
                passwordResetConfirmation.UserIdObj,
                passwordResetConfirmation.Token,
                passwordResetConfirmation.NewPasswordObj
            );

            await this.confirmPasswordReset.HandleAsync(command);

            return this.Ok();
        }

        // GET membership/passwordResetTokens/{userId}/{token}
        [AllowAnonymous]
        [Route("passwordResetTokens/{userId}/{*token}")]
        [ResponseType(typeof(bool))]
        public async Task<IHttpActionResult> GetPasswordResetTokenValidityAsync(Guid userId, string token)
        {
            var userIdObj = UserId.Parse(userId);
            var query = new IsPasswordResetTokenValidQuery(userIdObj, token);
            var tokenValid = await this.isPasswordResetTokenValid.HandleAsync(query);
            if (tokenValid)
            {
                return this.Ok();
            }

            return this.NotFound();
        }
    }
}
