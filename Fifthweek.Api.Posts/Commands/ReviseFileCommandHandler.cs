namespace Fifthweek.Api.Posts.Commands
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.CodeGeneration;

    [AutoConstructor]
    public partial class ReviseFileCommandHandler : ICommandHandler<ReviseFileCommand>
    {
        private readonly IRequesterSecurity requesterSecurity;
        private readonly IPostSecurity postSecurity;
        private readonly IFifthweekDbContext databaseContext;
        private readonly IScheduleGarbageCollectionStatement scheduleGarbageCollection;

        public async Task HandleAsync(ReviseFileCommand command)
        {
            command.AssertNotNull("command");

            var authenticatedUserId = await this.requesterSecurity.AuthenticateAsync(command.Requester);

            await this.postSecurity.AssertWriteAllowedAsync(authenticatedUserId, command.PostId);

            await this.ReviseFileAsync(command);

            // Only necessary when file changed, but simpler to invoke each time.
            await this.scheduleGarbageCollection.ExecuteAsync();
        }

        private Task ReviseFileAsync(ReviseFileCommand command)
        {
            var post = new Post(command.PostId.Value)
            {
                // CollectionId = command.CollectionId.Value, - Removed as this would require a queue defragmentation if post is queued. Unnecessary complexity for MVP.
                FileId = command.FileId.Value,
                Comment = command.Comment == null ? null : command.Comment.Value
            };

            return this.databaseContext.Database.Connection.UpdateAsync(post, Post.Fields.FileId | Post.Fields.Comment);
        }
    }
}