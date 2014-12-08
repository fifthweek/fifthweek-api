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

        public MembershipController(
            ICommandHandler<RegisterUserCommand> registerUser,
            IQueryHandler<GetUsernameAvailabilityQuery, bool> getUsernameAvailability)
        {
            if (registerUser == null)
            {
                throw new ArgumentNullException("registerUser");
            }

            if (getUsernameAvailability == null)
            {
                throw new ArgumentNullException("getUsernameAvailability");
            }

            this.registerUser = registerUser;
            this.getUsernameAvailability = getUsernameAvailability;
        }

        // POST membership/registrations
        [RequireHttps]
        [ConvertExceptionsToResponses]
        [ValidateModel]
        [AllowAnonymous]
        [Route("registrations")]
        public async Task<IHttpActionResult> PostRegistration(RegistrationData registrationData)
        {
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
        public async Task<IHttpActionResult> GetUsernameAvailability(string username)
        {
            var usernameAvailable = await this.getUsernameAvailability.HandleAsync(new GetUsernameAvailabilityQuery(username));
            if (usernameAvailable)
            {
                return this.Ok();
            }
                
            return this.NotFound();
        }
    }
}
