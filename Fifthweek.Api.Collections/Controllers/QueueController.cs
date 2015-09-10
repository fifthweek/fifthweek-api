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

    [RoutePrefix("queues"), AutoConstructor]
    public partial class QueueController : ApiController
    {
        private readonly ICommandHandler<CreateQueueCommand> createQueue;
        private readonly ICommandHandler<UpdateQueueCommand> updateQueue;
        private readonly ICommandHandler<DeleteQueueCommand> deleteQueue;
        private readonly IQueryHandler<GetLiveDateOfNewQueuedPostQuery, DateTime> getLiveDateOfNewQueuedPost;
        private readonly IRequesterContext requesterContext;
        private readonly IGuidCreator guidCreator;
        private readonly IRandom random;

        [Route]
        public async Task<QueueCreation> PostQueueAsync(NewQueueData newQueueData)
        {
            newQueueData.AssertBodyProvided("newQueueData");
            var newQueue = newQueueData.Parse();

            var requester = await this.requesterContext.GetRequesterAsync();
            var newQueueId = new QueueId(this.guidCreator.CreateSqlSequential());

            // Spread default release dates so posts are not delivered on same date as standard.
            var hourOfWeek = (byte)this.random.Next(HourOfWeek.MinValue, HourOfWeek.MaxValue + 1);
            var initialWeeklyReleaseTime = HourOfWeek.Parse(hourOfWeek);

            await this.createQueue.HandleAsync(
                new CreateQueueCommand(
                    requester,
                    newQueueId,
                    newQueue.BlogId,
                    newQueue.Name,
                    initialWeeklyReleaseTime));

            return new QueueCreation(newQueueId, initialWeeklyReleaseTime);
        }

        [Route("{queueId}")]
        public async Task<IHttpActionResult> PutQueueAsync(string queueId, [FromBody]UpdatedQueueData queueData)
        {
            queueId.AssertUrlParameterProvided("queueId");
            queueData.AssertBodyProvided("queueData");
            var queue = queueData.Parse();

            var requester = await this.requesterContext.GetRequesterAsync();
            var queueIdObject = new QueueId(queueId.DecodeGuid());

            await this.updateQueue.HandleAsync(
                new UpdateQueueCommand(
                    requester,
                    queueIdObject,
                    queue.Name,
                    queue.WeeklyReleaseSchedule));

            return this.Ok();
        }

        [Route("{queueId}")]
        public async Task<IHttpActionResult> DeleteQueueAsync(string queueId)
        {
            queueId.AssertUrlParameterProvided("queueId");

            var requester = await this.requesterContext.GetRequesterAsync();
            var queueIdObject = new QueueId(queueId.DecodeGuid());

            await this.deleteQueue.HandleAsync(new DeleteQueueCommand(requester, queueIdObject));

            return this.Ok();
        }

        [ResponseType(typeof(DateTime))]
        [Route("{queueId}/newQueuedPostLiveDate")]
        public async Task<DateTime> GetLiveDateOfNewQueuedPost(string queueId)
        {
            queueId.AssertUrlParameterProvided("queueId");

            var requester = await this.requesterContext.GetRequesterAsync();
            var queueIdObject = new QueueId(queueId.DecodeGuid());

            return await this.getLiveDateOfNewQueuedPost.HandleAsync(new GetLiveDateOfNewQueuedPostQuery(requester, queueIdObject));
        }
    }
}