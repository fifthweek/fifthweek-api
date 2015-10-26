namespace Fifthweek.Api.Posts.Commands
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
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
        private readonly IScheduleGarbageCollectionStatement scheduleGarbageCollection;
        private readonly IFileSecurity fileSecurity;
        private readonly IRevisePostDbStatement revisePostDbStatement;

        public async Task HandleAsync(RevisePostCommand command)
        {
            command.AssertNotNull("command");

            var authenticatedUserId = await this.requesterSecurity.AuthenticateAsync(command.Requester);

            await this.postSecurity.AssertWriteAllowedAsync(authenticatedUserId, command.PostId);
           
            if (command.FileIds.All(v => !v.Equals(command.PreviewImageId)))
            {
                throw new BadRequestException("Preview Image ID must exist in File IDs list.");
            }

            if (command.FileIds.Count == 0 && command.Content == null)
            {
                throw new BadRequestException("Please provide some content.");
            }

            foreach (var fileId in command.FileIds)
            {
                await this.fileSecurity.AssertReferenceAllowedAsync(authenticatedUserId, fileId);
            }

            await this.revisePostDbStatement.ExecuteAsync(
                command.PostId,
                command.Content,
                command.PreviewText,
                command.PreviewImageId,
                command.FileIds,
                command.PreviewWordCount,
                command.WordCount,
                command.ImageCount,
                command.FileCount);

            // Only necessary when file changed, but simpler to invoke each time.
            await this.scheduleGarbageCollection.ExecuteAsync();
        }
    }
}