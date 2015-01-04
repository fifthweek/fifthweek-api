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
        private readonly IQueryHandler<GetUsernameAvailabilityQuery, bool> getUsernameAvailability;

        public MembershipController(
            ICommandHandler<RegisterUserCommand> registerUser,
            ICommandHandler<RequestPasswordResetCommand> requestPasswordReset,
            ICommandHandler<ConfirmPasswordResetCommand> confirmPasswordReset,
            IQueryHandler<GetUsernameAvailabilityQuery, bool> getUsernameAvailability)
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

            if (getUsernameAvailability == null)
            {
                throw new ArgumentNullException("getUsernameAvailability");
            }

            this.registerUser = registerUser;
            this.requestPasswordReset = requestPasswordReset;
            this.confirmPasswordReset = confirmPasswordReset;
            this.getUsernameAvailability = getUsernameAvailability;
            }

        // POST membership/registrations
        [AllowAnonymous]
        [Route("registrations")]
        public async Task<IHttpActionResult> PostRegistrationAsync(RegistrationData registration)
        {
            registration.Parse();

            var command = new RegisterUserCommand(
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

            var query = new GetUsernameAvailabilityQuery(NormalizedUsername.Normalize(usernameObj));
            var usernameAvailable = await this.getUsernameAvailability.HandleAsync(query);
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
    }
}
