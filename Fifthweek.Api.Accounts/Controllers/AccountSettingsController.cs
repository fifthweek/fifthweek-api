namespace Fifthweek.Api.Accounts.Controllers
{
    using System.Threading.Tasks;
    using System.Web.Http;
    using System.Web.Http.Description;

    using Fifthweek.Api.Accounts.Commands;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.Api.Identity.OAuth;

    [AutoConstructor]
    [RoutePrefix("accountsettings")]
    public partial class AccountSettingsController : ApiController
    {
        private readonly IUserContext userContext;

        [ResponseType(typeof(AccountSettingsData))]
        [Route("{userId}")]
        public async Task Get(string userId)
        {
            var requestedUserId = new UserId(userId.DecodeGuid());
            var authenticatedUserId = this.userContext.GetUserId();


        }

        [Route("{userId}")]
        public async Task Put(string userId, [FromBody]UpdatedAccountSettingsData updatedAccountSettings)
        {
            var authenticatedUserId = this.userContext.GetUserId();
            var requestedUserId = new UserId(userId.DecodeGuid());

            var command = new UpdateAccountSettingsCommand(
                authenticatedUserId,
                requestedUserId,
                updatedAccountSettings.NewUsername,
                updatedAccountSettings.NewEmail,
                updatedAccountSettings.NewProfileImageId);

        }
    }
}