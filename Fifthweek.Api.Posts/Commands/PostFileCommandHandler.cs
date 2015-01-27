namespace Fifthweek.Api.Posts.Commands
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Collections;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;

    [AutoConstructor]
    public partial class PostFileCommandHandler : ICommandHandler<PostFileCommand>
    {
        private readonly ICollectionSecurity collectionSecurity;
        private readonly IFileSecurity fileSecurity;
        private readonly IRequesterSecurity requesterSecurity;
        private readonly IPostFileTypeChecks postFileTypeChecks;
        private readonly IPostToCollectionDbStatement postToCollectionDbStatement;

        public async Task HandleAsync(PostFileCommand command)
        {
            command.AssertNotNull("command");

            var authenticatedUserId = await this.requesterSecurity.AuthenticateAsync(command.Requester);

            await this.collectionSecurity.AssertPostingAllowedAsync(authenticatedUserId, command.CollectionId);

            await this.fileSecurity.AssertUsageAllowedAsync(authenticatedUserId, command.FileId);

            await this.postFileTypeChecks.AssertValidForFilePostAsync(command.FileId);

            await this.postToCollectionDbStatement.ExecuteAsync(
                command.NewPostId,
                command.CollectionId,
                command.Comment,
                command.ScheduledPostDate,
                command.IsQueued,
                command.FileId,
                false,
                DateTime.UtcNow);
        }
    }
}