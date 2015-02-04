namespace Fifthweek.Api.Posts
{
    using System;
    using System.Threading.Tasks;
    using System.Transactions;

    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.CodeGeneration;

    [AutoConstructor]
    public partial class RemoveFromQueueIfRequiredDbStatement : IRemoveFromQueueIfRequiredDbStatement
    {
        private readonly ITryGetQueuedPostCollectionDbStatement tryGetQueuedPostCollection;
        private readonly IGetWeeklyReleaseScheduleDbStatement getWeeklyReleaseSchedule;
        private readonly IDefragmentQueueDbStatement defragmentQueue;

        public async Task ExecuteAsync(PostId postId, DateTime now, Func<Task> potentialRemovalOperation)
        {
            postId.AssertNotNull("postId");
            now.AssertUtc("now");
            potentialRemovalOperation.AssertNotNull("potentialRemovalOperation");

            var queuedCollectionId = await this.tryGetQueuedPostCollection.ExecuteAsync(postId, now);
            if (queuedCollectionId == null)
            {
                await potentialRemovalOperation();
            }
            else
            {
                var weeklyReleaseSchedule = await this.getWeeklyReleaseSchedule.ExecuteAsync(queuedCollectionId);

                using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    await potentialRemovalOperation();
                    await this.defragmentQueue.ExecuteAsync(queuedCollectionId, weeklyReleaseSchedule, now);

                    transaction.Complete();
                }
            }
        }
    }
}