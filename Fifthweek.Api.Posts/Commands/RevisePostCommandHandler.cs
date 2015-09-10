namespace Fifthweek.Api.Posts.Commands
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;

    [AutoConstructor]
    public partial class RevisePostCommandHandler : ICommandHandler<RevisePostCommand>
    {
        private readonly IRequesterSecurity requesterSecurity;
        private readonly IPostSecurity postSecurity;
        private readonly IFifthweekDbConnectionFactory connectionFactory;
        private readonly IScheduleGarbageCollectionStatement scheduleGarbageCollection;

        public async Task HandleAsync(RevisePostCommand command)
        {
            command.AssertNotNull("command");

            var authenticatedUserId = await this.requesterSecurity.AuthenticateAsync(command.Requester);

            await this.postSecurity.AssertWriteAllowedAsync(authenticatedUserId, command.PostId);

            await this.ReviseFileAsync(command);

            // Only necessary when file changed, but simpler to invoke each time.
            await this.scheduleGarbageCollection.ExecuteAsync();
        }

        private async Task ReviseFileAsync(RevisePostCommand command)
        {
            var post = new Post(command.PostId.Value)
            {
                // QueueId = command.QueueId.Value, - Removed as this would require a queue defragmentation if post is already queued. Unnecessary complexity for MVP.
                FileId = command.FileId == null ? (Guid?)null : command.FileId.Value,
                ImageId = command.ImageId == null ? (Guid?)null : command.ImageId.Value,
                Comment = command.Comment == null ? null : command.Comment.Value
            };

            using (var connection = this.connectionFactory.CreateConnection())
            {
                await connection.UpdateAsync(post, Post.Fields.FileId | Post.Fields.ImageId | Post.Fields.Comment);
            }
        }
    }
}