namespace Fifthweek.Api.Posts.Commands
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;

    [AutoConstructor]
    public partial class RescheduleWithQueueCommandHandler : ICommandHandler<RescheduleWithQueueCommand>
    {
        private readonly IRequesterSecurity requesterSecurity;
        private readonly IPostSecurity postSecurity;
        private readonly IMovePostToQueueDbStatement movePostToQueue;
        private readonly IDefragmentQueueIfRequiredDbStatement defragmentQueueIfRequired;
        private readonly ITimestampCreator timestampCreator;

        public async Task HandleAsync(RescheduleWithQueueCommand command)
        {
            command.AssertNotNull("command");

            var userId = await this.requesterSecurity.AuthenticateAsync(command.Requester);
            await this.postSecurity.AssertWriteAllowedAsync(userId, command.PostId);

            await this.RescheduleWithQueueAsync(command.PostId, command.QueueId);
        }

        private Task RescheduleWithQueueAsync(PostId postId, QueueId queueId)
        {
            var now = this.timestampCreator.Now();

            return this.defragmentQueueIfRequired.ExecuteAsync(
                postId,
                now,
                () => this.movePostToQueue.ExecuteAsync(postId, queueId));
        }
    }
}