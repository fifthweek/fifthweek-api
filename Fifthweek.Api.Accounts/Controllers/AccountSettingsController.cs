namespace Fifthweek.Api.Accounts.Controllers
{
    using System.Threading.Tasks;
    using System.Web.Http;
    using System.Web.Http.Description;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.Api.Identity.OAuth;

    [AutoConstructor]
    [RoutePrefix("accountsettings")]
    public partial class AccountSettingsController : ApiController
    {
        private readonly IUserContext userContext;

        [Authorize]
        [ResponseType(typeof(AccountSettingsData))]
        [Route("{userId}")]
        public async Task Get(string userId)
        {
            var requestedUserId = new UserId(userId.DecodeGuid());
            var authenticatedUserId = this.userContext.GetUserId();

            if (!requestedUserId.Equals(authenticatedUserId))
            {
                throw new ForbiddenException("User " + authenticatedUserId + " does may not get the account settings for user " + requestedUserId);
            }


        }

        [Authorize]
        [Route("{userId}")]
        public async Task Put(string userId)
        {
            var requestedUserId = new UserId(userId.DecodeGuid());
            var authenticatedUserId = this.userContext.GetUserId();

            if (!requestedUserId.Equals(authenticatedUserId))
            {
                throw new ForbiddenException("User " + authenticatedUserId + " does may not get the account settings for user " + requestedUserId);
            }
        }
    }
}