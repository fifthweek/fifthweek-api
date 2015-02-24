namespace Fifthweek.Api.Identity.Membership.Controllers
{
    using System.Threading.Tasks;
    using System.Web.Http;

    using Fifthweek.Api.Identity.Membership.Commands;
    using Fifthweek.Api.Identity.Membership.Queries;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;

    [AutoConstructor]
    [RoutePrefix("accountSettings")]
    public partial class AccountSettingsController : ApiController
    {
        private readonly IRequesterContext requesterContext;

        private readonly ICommandHandler<UpdateAccountSettingsCommand> updateAccountSettings;

        private readonly IQueryHandler<GetAccountSettingsQuery, GetAccountSettingsResult> getAccountSettings;

        [Route("{userId}")]
        public async Task<GetAccountSettingsResult> Get(string userId)
        {
            userId.AssertUrlParameterProvided("userId");

            var requestedUserId = new UserId(userId.DecodeGuid());
            var requester = this.requesterContext.GetRequester();

            var query = new GetAccountSettingsQuery(requester, requestedUserId);
            var result = await this.getAccountSettings.HandleAsync(query);
            return result;
        }

        [Route("{userId}")]
        public async Task Put(string userId, [FromBody]UpdatedAccountSettings updatedAccountSettingsData)
        {
            userId.AssertUrlParameterProvided("userId");
            updatedAccountSettingsData.AssertBodyProvided("updatedAccountSettings");

            var updatedAccountSettings = updatedAccountSettingsData.Parse();
            var requester = this.requesterContext.GetRequester();
            var requestedUserId = new UserId(userId.DecodeGuid());
            
            var newProfileImageId 
                = updatedAccountSettingsData.NewProfileImageId == null
                ? null
                : new FileId(updatedAccountSettingsData.NewProfileImageId.DecodeGuid());

            var command = new UpdateAccountSettingsCommand(
                requester,
                requestedUserId,
                updatedAccountSettings.NewUsername,
                updatedAccountSettings.NewEmail,
                updatedAccountSettings.NewPassword,
                newProfileImageId);

            await this.updateAccountSettings.HandleAsync(command);
        }
    }
}