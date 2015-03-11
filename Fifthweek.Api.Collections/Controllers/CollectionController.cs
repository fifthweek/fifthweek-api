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
    using Fifthweek.Shared;

    [RoutePrefix("collections"), AutoConstructor]
    public partial class CollectionController : ApiController
    {
        private readonly ICommandHandler<CreateCollectionCommand> createCollection;
        private readonly ICommandHandler<UpdateCollectionCommand> updateCollection;
        private readonly ICommandHandler<DeleteCollectionCommand> deleteCollection;
        private readonly IQueryHandler<GetLiveDateOfNewQueuedPostQuery, DateTime> getLiveDateOfNewQueuedPost;
        private readonly IRequesterContext requesterContext;
        private readonly IGuidCreator guidCreator;
        private readonly IRandom random;

        [Route]
        public async Task<CollectionCreation> PostCollectionAsync(NewCollectionData newCollectionData)
        {
            newCollectionData.AssertBodyProvided("newCollectionData");
            var newCollection = newCollectionData.Parse();

            var requester = this.requesterContext.GetRequester();
            var newCollectionId = new CollectionId(this.guidCreator.CreateSqlSequential());

            // Spread default release dates so posts are not delivered on same date as standard.
            var hourOfWeek = (byte)this.random.Next(HourOfWeek.MinValue, HourOfWeek.MaxValue + 1);
            var initialWeeklyReleaseTime = HourOfWeek.Parse(hourOfWeek);

            await this.createCollection.HandleAsync(
                new CreateCollectionCommand(
                    requester,
                    newCollectionId,
                    newCollection.ChannelId,
                    newCollection.Name,
                    initialWeeklyReleaseTime));

            return new CollectionCreation(newCollectionId, initialWeeklyReleaseTime);
        }

        [Route("{collectionId}")]
        public async Task<IHttpActionResult> PutCollectionAsync(string collectionId, [FromBody]UpdatedCollectionData collectionData)
        {
            collectionId.AssertUrlParameterProvided("collectionId");
            collectionData.AssertBodyProvided("collectionData");
            var collection = collectionData.Parse();

            var requester = this.requesterContext.GetRequester();
            var collectionIdObject = new CollectionId(collectionId.DecodeGuid());

            await this.updateCollection.HandleAsync(
                new UpdateCollectionCommand(
                    requester,
                    collectionIdObject,
                    collection.ChannelId,
                    collection.Name,
                    collection.WeeklyReleaseSchedule));

            return this.Ok();
        }

        [Route("{collectionId}")]
        public async Task<IHttpActionResult> DeleteCollectionAsync(string collectionId)
        {
            collectionId.AssertUrlParameterProvided("collectionId");

            var requester = this.requesterContext.GetRequester();
            var collectionIdObject = new CollectionId(collectionId.DecodeGuid());

            await this.deleteCollection.HandleAsync(new DeleteCollectionCommand(requester, collectionIdObject));

            return this.Ok();
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