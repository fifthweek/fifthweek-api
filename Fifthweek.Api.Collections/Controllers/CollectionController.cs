namespace Fifthweek.Api.Collections.Controllers
{
    using System;
    using System.Threading.Tasks;
    using System.Web.Http;
    using System.Web.Http.Description;

    using Fifthweek.Api.Collections.Queries;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.Api.Identity.OAuth;
    using Fifthweek.CodeGeneration;

    [RoutePrefix("collections"), AutoConstructor]
    public partial class CollectionController : ApiController
    {
        private readonly IQueryHandler<GetLiveDateOfNewQueuedPostQuery, DateTime> getLiveDateOfNewQueuedPost;
        private readonly IUserContext userContext;

        [ResponseType(typeof(DateTime))]
        [Route("{collectionId}/newQueuedPostLiveDate")]
        public async Task<DateTime> GetLiveDateOfNewQueuedPost(string collectionId)
        {
            collectionId.AssertUrlParameterProvided("collectionId");

            var requester = this.userContext.GetRequester();
            var collectionIdObject = new CollectionId(collectionId.DecodeGuid());

            return await this.getLiveDateOfNewQueuedPost.HandleAsync(new GetLiveDateOfNewQueuedPostQuery(requester, collectionIdObject));
        }
    }
}