namespace Fifthweek.Api.Posts.Commands
{
    using System;
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
        private readonly IPostFileTypeChecks postFileTypeChecks;
        private readonly IPostToChannelDbStatement postToChannel;

        public async Task HandleAsync(PostToChannelCommand command)
        {
            command.AssertNotNull("command");

            if (command.FileId == null && command.ImageId == null && command.Comment == null)
            {
                throw new BadRequestException("Some content must be provided.");
            }

            var authenticatedUserId = await this.requesterSecurity.AuthenticateAsync(command.Requester);

            await this.queueSecurity.AssertWriteAllowedAsync(authenticatedUserId, command.QueueId);

            await this.fileSecurity.AssertReferenceAllowedAsync(authenticatedUserId, command.FileId);

            if (command.FileId != null)
            {
                await this.postFileTypeChecks.AssertValidForFilePostAsync(command.FileId);
            }

            if (command.ImageId != null)
            {
                await this.postFileTypeChecks.AssertValidForImagePostAsync(command.ImageId);
            }

            await this.postToChannel.ExecuteAsync(
                command.NewPostId,
                command.ChannelId,
                command.Comment,
                command.ScheduledPostTime,
                command.QueueId,
                command.FileId,
                command.ImageId,
                command.Timestamp);
        }
    }
}