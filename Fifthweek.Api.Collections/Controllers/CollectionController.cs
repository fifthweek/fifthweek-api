namespace Fifthweek.Api.Collections.Controllers
{
    using System;
    using System.Threading.Tasks;
    using System.Web.Http;
    using System.Web.Http.Description;

    using Fifthweek.Api.Collections.Commands;
    using Fifthweek.Api.Collections.Queries;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;

    [RoutePrefix("collections"), AutoConstructor]
    public partial class CollectionController : ApiController
    {
        private readonly ICommandHandler<CreateCollectionCommand> createCollection;
        private readonly ICommandHandler<UpdateCollectionCommand> updateCollection;
        private readonly IQueryHandler<GetLiveDateOfNewQueuedPostQuery, DateTime> getLiveDateOfNewQueuedPost;
        private readonly IRequesterContext requesterContext;
        private readonly IGuidCreator guidCreator;

        [Route]
        public async Task<CollectionId> PostCollectionAsync(NewCollectionData newCollectionData)
        {
            newCollectionData.AssertBodyProvided("newCollectionData");
            var newCollection = newCollectionData.Parse();

            var requester = this.requesterContext.GetRequester();
            var newCollectionId = new CollectionId(this.guidCreator.CreateSqlSequential());

            await this.createCollection.HandleAsync(
                new CreateCollectionCommand(
                    requester,
                    newCollectionId,
                    newCollection.ChannelId,
                    newCollection.Name));

            return newCollectionId;
        }

        [ResponseType(typeof(DateTime))]
        [Route("{collectionId}/newQueuedPostLiveDate")]
        public async Task<DateTime> GetLiveDateOfNewQueuedPost(string collectionId)
        {
            collectionId.AssertUrlParameterProvided("collectionId");

            var requester = this.requesterContext.GetRequester();
            var collectionIdObject = new CollectionId(collectionId.DecodeGuid());

            return await this.getLiveDateOfNewQueuedPost.HandleAsync(new GetLiveDateOfNewQueuedPostQuery(requester, collectionIdObject));
        }
    }
}