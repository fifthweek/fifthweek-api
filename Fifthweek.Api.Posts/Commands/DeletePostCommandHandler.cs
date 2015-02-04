namespace Fifthweek.Api.Posts.Commands
{
    using System;
    using System.Threading.Tasks;

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
        private readonly IDeletePostDbStatement deletePost;
        private readonly IRemoveFromQueueIfRequiredDbStatement removeFromQueueIfRequired;
        private readonly IScheduleGarbageCollectionStatement scheduleGarbageCollection;

        public async Task HandleAsync(DeletePostCommand command)
        {
            command.AssertNotNull("command");

            var userId = await this.requesterSecurity.AuthenticateAsync(command.Requester);
            await this.postSecurity.AssertWriteAllowedAsync(userId, command.PostId);

            var now = DateTime.UtcNow;

            await this.removeFromQueueIfRequired.ExecuteAsync(
                command.PostId,
                now,
                () => this.deletePost.ExecuteAsync(command.PostId));

            await this.scheduleGarbageCollection.ExecuteAsync();
        }
    }
}