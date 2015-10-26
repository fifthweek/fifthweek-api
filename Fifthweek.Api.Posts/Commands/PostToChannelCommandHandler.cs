namespace Fifthweek.Api.Posts.Commands
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;

    [AutoConstructor]
    public partial class PostToChannelCommandHandler : ICommandHandler<PostToChannelCommand>
    {
        private readonly IQueueSecurity queueSecurity;
        private readonly IFileSecurity fileSecurity;
        private readonly IRequesterSecurity requesterSecurity;
        private readonly IPostToChannelDbStatement postToChannel;

        public async Task HandleAsync(PostToChannelCommand command)
        {
            command.AssertNotNull("command");

            var authenticatedUserId = await this.requesterSecurity.AuthenticateAsync(command.Requester);

            if (command.FileIds.All(v => !v.Equals(command.PreviewImageId)))
            {
                throw new BadRequestException("Preview Image ID must exist in File IDs list.");
            }

            if (command.FileIds.Count == 0 && command.PreviewText == null)
            {
                throw new BadRequestException("Please provide some content.");
            }

            if (command.QueueId != null)
            {
                await this.queueSecurity.AssertWriteAllowedAsync(authenticatedUserId, command.QueueId);
            }

            foreach (var fileId in command.FileIds)
            {
                await this.fileSecurity.AssertReferenceAllowedAsync(authenticatedUserId, fileId);
            }

            await this.postToChannel.ExecuteAsync(
                command.NewPostId,
                command.ChannelId,
                command.Content,
                command.ScheduledPostTime,
                command.QueueId,
                command.PreviewText,
                command.PreviewImageId,
                command.FileIds,
                command.PreviewWordCount,
                command.WordCount,
                command.ImageCount,
                command.FileCount,
                command.Timestamp);
        }
    }
}