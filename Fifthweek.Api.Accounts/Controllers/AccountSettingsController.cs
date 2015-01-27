namespace Fifthweek.Api.Accounts.Controllers
{
    using System.Threading.Tasks;
    using System.Web.Http;

    using Fifthweek.Api.Accounts.Commands;
    using Fifthweek.Api.Accounts.Queries;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement;
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.Api.Identity.OAuth;
    using Fifthweek.CodeGeneration;

    using UserId = Fifthweek.Api.Identity.Shared.Membership.UserId;

    [AutoConstructor]
    [RoutePrefix("accountsettings")]
    public partial class AccountSettingsController : ApiController
    {
        private readonly IUserContext userContext;

        private readonly ICommandHandler<UpdateAccountSettingsCommand> updateAccountSettings;

        private readonly IQueryHandler<GetAccountSettingsQuery, GetAccountSettingsResult> getAccountSettings;

        [Route("{userId}")]
        public async Task<AccountSettingsResponse> Get(string userId)
        {
            userId.AssertUrlParameterProvided("userId");

            var requestedUserId = new UserId(userId.DecodeGuid());
            var requester = this.userContext.GetRequester();

            var query = new GetAccountSettingsQuery(requester, requestedUserId);
            var result = await this.getAccountSettings.HandleAsync(query);

            return new AccountSettingsResponse(result.Email.Value, result.ProfileImageFileId.Value.EncodeGuid());
        }

        [Route("{userId}")]
        public async Task Put(string userId, [FromBody]UpdatedAccountSettings updatedAccountSettings)
        {
            userId.AssertUrlParameterProvided("userId");
            updatedAccountSettings.AssertBodyProvided("updatedAccountSettings");

            var requester = this.userContext.GetRequester();
            var requestedUserId = new UserId(userId.DecodeGuid());
            
            updatedAccountSettings.Parse();
            var newProfileImageId = new FileId(updatedAccountSettings.NewProfileImageId.DecodeGuid());

            var command = new UpdateAccountSettingsCommand(
                requester,
                requestedUserId,
                updatedAccountSettings.NewUsernameObject,
                updatedAccountSettings.NewEmailObject,
                updatedAccountSettings.NewPasswordObject,
                newProfileImageId);

            await this.updateAccountSettings.HandleAsync(command);
        }
    }
}