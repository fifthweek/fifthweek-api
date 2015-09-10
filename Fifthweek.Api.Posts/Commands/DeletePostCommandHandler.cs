namespace Fifthweek.Api.Posts.Commands
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;

    [AutoConstructor]
    public partial class DeletePostCommandHandler : ICommandHandler<DeletePostCommand>
    {
        private readonly IRequesterSecurity requesterSecurity;
        private readonly IPostSecurity postSecurity;
        private readonly IDeletePostDbStatement deletePost;
        private readonly IDefragmentQueueIfRequiredDbStatement defragmentQueueIfRequired;
        private readonly IScheduleGarbageCollectionStatement scheduleGarbageCollection;
        private readonly ITimestampCreator timestampCreator;

        public async Task HandleAsync(DeletePostCommand command)
        {
            command.AssertNotNull("command");

            var userId = await this.requesterSecurity.AuthenticateAsync(command.Requester);
            await this.postSecurity.AssertWriteAllowedAsync(userId, command.PostId);

            var now = this.timestampCreator.Now();

            await this.defragmentQueueIfRequired.ExecuteAsync(
                command.PostId,
                now,
                () => this.deletePost.ExecuteAsync(command.PostId));

            await this.scheduleGarbageCollection.ExecuteAsync();
        }
    }
}