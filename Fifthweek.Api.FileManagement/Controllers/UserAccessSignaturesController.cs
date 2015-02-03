namespace Fifthweek.Api.FileManagement.Controllers
{
    using System.Threading.Tasks;
    using System.Web.Http;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement.Queries;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;

    [AutoConstructor]
    [RoutePrefix("userAccessSignatures")]
    public partial class UserAccessSignaturesController
    {
        private readonly IQueryHandler<GetUserAccessSignaturesQuery, UserAccessSignatures> getUserAccessSignatures;

        private readonly IRequesterContext requesterContext;

        [Route("{userId}")]
        public async Task<UserAccessSignatures> Get(string userId)
        {
            var requestedUserId = string.IsNullOrWhiteSpace(userId) ? null : new UserId(userId.DecodeGuid());
            var requester = this.requesterContext.GetRequester();

            var userState = await this.getUserAccessSignatures.HandleAsync(new GetUserAccessSignaturesQuery(requester, requestedUserId));

            return userState;
        }
    }
}