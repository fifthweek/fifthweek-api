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
    public partial class UserAccessSignaturesController : ApiController
    {
        private readonly IQueryHandler<GetUserAccessSignaturesQuery, UserAccessSignatures> getUserAccessSignatures;

        private readonly IRequesterContext requesterContext;

        [Route("")]
        public Task<UserAccessSignatures> GetForVisitor()
        {
            return this.GetForUser();
        }

        [Route("{userId}")]
        public Task<UserAccessSignatures> GetForUser(string userId)
        {
            userId.AssertUrlParameterProvided("userId");

            var requestedUserId = new UserId(userId.DecodeGuid());

            return this.GetForUser(requestedUserId);
        }

        private Task<UserAccessSignatures> GetForUser(UserId userId = null)
        {
            var requester = this.requesterContext.GetRequester();

            return this.getUserAccessSignatures.HandleAsync(new GetUserAccessSignaturesQuery(requester, userId));
        }
    }
}