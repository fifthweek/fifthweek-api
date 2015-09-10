namespace Fifthweek.Api.Posts
{
    using System;
    using System.Threading.Tasks;
    using System.Transactions;

    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;

    [AutoConstructor]
    public partial class DefragmentQueueIfRequiredDbStatement : IDefragmentQueueIfRequiredDbStatement
    {
        private readonly ITryGetPostQueueIdStatement tryGetPostQueueId;
        private readonly IGetWeeklyReleaseScheduleDbStatement getWeeklyReleaseSchedule;
        private readonly IDefragmentQueueDbStatement defragmentQueue;

        public async Task ExecuteAsync(PostId postId, DateTime now, Func<Task> potentialRemovalOperation)
        {
            postId.AssertNotNull("postId");
            now.AssertUtc("now");
            potentialRemovalOperation.AssertNotNull("potentialRemovalOperation");

            var queuedCollectionId = await this.tryGetPostQueueId.ExecuteAsync(postId, now);
            if (queuedCollectionId == null)
            {
                await potentialRemovalOperation();
            }
            else
            {
                var weeklyReleaseSchedule = await this.getWeeklyReleaseSchedule.ExecuteAsync(queuedCollectionId);

                using (var transaction = TransactionScopeBuilder.CreateAsync())
                {
                    await potentialRemovalOperation();
                    await this.defragmentQueue.ExecuteAsync(queuedCollectionId, weeklyReleaseSchedule, now);

                    transaction.Complete();
                }
            }
        }
    }
}