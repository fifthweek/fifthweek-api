using System;
using System.Web.Http;
using System.Web.Http.Description;
using Fifthweek.Api.Queries;
using Fifthweek.Api.QueryHandlers;

namespace Fifthweek.Api.Controllers
{
    using System.Threading.Tasks;

    using Fifthweek.Api.CommandHandlers;
    using Fifthweek.Api.Commands;
    using Fifthweek.Api.Models;

    [RoutePrefix("membership")]
    public class MembershipController : ApiController
    {
        private readonly ICommandHandler<RegisterUserCommand> registerUser;
        private readonly IQueryHandler<GetUsernameAvailabilityQuery, bool> getUsernameAvailability;
        private readonly IUserInputNormalization userInputNormalization;

        public MembershipController(
            ICommandHandler<RegisterUserCommand> registerUser,
            IQueryHandler<GetUsernameAvailabilityQuery, bool> getUsernameAvailability,
            IUserInputNormalization userInputNormalization)
        {
            if (registerUser == null)
            {
                throw new ArgumentNullException("registerUser");
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
            this.getUsernameAvailability = getUsernameAvailability;
            this.userInputNormalization = userInputNormalization;
        }

        // POST membership/registrations
        [RequireHttps]
        [ConvertExceptionsToResponses]
        [ValidateModel]
        [AllowAnonymous]
        [Route("registrations")]
        public async Task<IHttpActionResult> PostRegistrationAsync(RegistrationData registrationData)
        {
            var normalizedEmail = this.userInputNormalization.NormalizeEmailAddress(registrationData.Email);
            var normalizedUsername = this.userInputNormalization.NormalizeUsername(registrationData.Username);
            if (normalizedEmail != registrationData.Email ||
                normalizedUsername != registrationData.Username)
            {
                registrationData = new RegistrationData
                {
                    Email = normalizedEmail,
                    Username = normalizedUsername,
                    ExampleWork = registrationData.ExampleWork,
                    Password = registrationData.Password
                };
            }

            await this.registerUser.HandleAsync(new RegisterUserCommand(registrationData));
            return this.Ok();
        }

        // GET membership/availableUsernames
        [RequireHttps]
        [ConvertExceptionsToResponses]
        [ValidateModel]
        [AllowAnonymous]
        [Route("availableUsernames/{username}")]
        [ResponseType(typeof(bool))]
        public async Task<IHttpActionResult> GetUsernameAvailabilityAsync(string username)
        {
            var normalizedUsername = this.userInputNormalization.NormalizeUsername(username);
            var usernameAvailable = await this.getUsernameAvailability.HandleAsync(new GetUsernameAvailabilityQuery(normalizedUsername));
            if (usernameAvailable)
            {
                return this.Ok();
            }
                
            return this.NotFound();
        }
    }
}
