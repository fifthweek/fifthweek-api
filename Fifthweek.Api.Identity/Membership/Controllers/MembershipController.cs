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
        private readonly IQueryHandler<GetUsernameAvailabilityQuery, bool> getUsernameAvailability;
        private readonly IUserInputNormalization userInputNormalization;

        public MembershipController(
            ICommandHandler<RegisterUserCommand> registerUser,
            ICommandHandler<RequestPasswordResetCommand> requestPasswordReset,
            IQueryHandler<GetUsernameAvailabilityQuery, bool> getUsernameAvailability,
            IUserInputNormalization userInputNormalization)
        {
            if (registerUser == null)
            {
                throw new ArgumentNullException("registerUser");
            }
            if (requestPasswordReset == null)
            {
                throw new ArgumentNullException("requestPasswordReset");
            }

            if (getUsernameAvailability == null)
            {
                throw new ArgumentNullException("getUsernameAvailability");
            }

            if (userInputNormalization == null)
            {
                throw new ArgumentNullException("userInputNormalization");
            }

            this.registerUser = registerUser;
            this.requestPasswordReset = requestPasswordReset;
            this.getUsernameAvailability = getUsernameAvailability;
            this.userInputNormalization = userInputNormalization;
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
                this.userInputNormalization.NormalizeUsername(registration.Username),
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
            var query = new GetUsernameAvailabilityQuery(
                this.userInputNormalization.NormalizeUsername(username));

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
                passwordResetRequest.Username
            );

            await this.requestPasswordReset.HandleAsync(command);

            return this.Ok();
        }
    }
}
