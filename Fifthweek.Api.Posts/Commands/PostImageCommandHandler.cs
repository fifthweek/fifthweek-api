namespace Fifthweek.Api.Posts.Commands
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;

    [AutoConstructor]
    public partial class PostImageCommandHandler : ICommandHandler<PostImageCommand>
    {
        private readonly ICollectionSecurity collectionSecurity;
        private readonly IFileSecurity fileSecurity;
        private readonly IRequesterSecurity requesterSecurity;
        private readonly IPostFileTypeChecks postFileTypeChecks;
        private readonly IPostToCollectionDbStatement postToCollectionDbStatement;

        public async Task HandleAsync(PostImageCommand command)
        {
            command.AssertNotNull("command");

            var authenticatedUserId = await this.requesterSecurity.AuthenticateAsync(command.Requester);

            await this.collectionSecurity.AssertWriteAllowedAsync(authenticatedUserId, command.CollectionId);

            await this.fileSecurity.AssertReferenceAllowedAsync(authenticatedUserId, command.ImageFileId);

            await this.postFileTypeChecks.AssertValidForImagePostAsync(command.ImageFileId);

            await this.postToCollectionDbStatement.ExecuteAsync(
                command.NewPostId,
                command.CollectionId,
                command.Comment,
                command.ScheduledPostDate,
                command.IsQueued,
                command.ImageFileId,
                true,
                DateTime.UtcNow);
        }
    }
}