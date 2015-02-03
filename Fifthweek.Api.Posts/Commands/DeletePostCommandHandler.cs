namespace Fifthweek.Api.Posts.Commands
{
    using System;
    using System.Threading.Tasks;
    using System.Transactions;

    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.CodeGeneration;

    [AutoConstructor]
    public partial class DeletePostCommandHandler : ICommandHandler<DeletePostCommand>
    {
        private readonly IRequesterSecurity requesterSecurity;
        private readonly IPostSecurity postSecurity;
        private readonly ITryGetQueuedPostCollectionDbStatement tryGetQueuedPostCollection;
        private readonly IGetWeeklyReleaseScheduleDbStatement getWeeklyReleaseSchedule;
        private readonly IDeletePostDbStatement deletePost;
        private readonly IDefragmentQueueDbStatement defragmentQueue;
        private readonly IScheduleGarbageCollectionStatement scheduleGarbageCollection;

        public async Task HandleAsync(DeletePostCommand command)
        {
            command.AssertNotNull("command");

            var userId = await this.requesterSecurity.AuthenticateAsync(command.Requester);

            await this.postSecurity.AssertWriteAllowedAsync(userId, command.PostId);

            var now = DateTime.UtcNow;
            var queuedCollectionId = await this.tryGetQueuedPostCollection.ExecuteAsync(command.PostId, now);
            if (queuedCollectionId == null)
            {
                await this.deletePost.ExecuteAsync(command.PostId);
            }
            else
            {
                var weeklyReleaseSchedule = await this.getWeeklyReleaseSchedule.ExecuteAsync(queuedCollectionId);

                using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    await this.deletePost.ExecuteAsync(command.PostId);
                    await this.defragmentQueue.ExecuteAsync(queuedCollectionId, weeklyReleaseSchedule, now);

                    transaction.Complete();
                }
            }

            await this.scheduleGarbageCollection.ExecuteAsync();
        }
    }
}