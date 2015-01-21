namespace Fifthweek.Api.Posts.Commands
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Azure;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement;
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.CodeGeneration;
    using Fifthweek.WebJobs.Deletions.Shared;

    [AutoConstructor]
    public partial class DeletePostCommandHandler : ICommandHandler<DeletePostCommand>
    {
        private readonly IScheduleGarbageCollectionStatement scheduleGarbageCollection;

        private readonly IPostSecurity postSecurity;

        private readonly IDeletePostDbStatement deletePost;

        public async Task HandleAsync(DeletePostCommand command)
        {
            command.AssertNotNull("command");

            UserId userId;
            command.Requester.AssertAuthenticated(out userId);

            await this.postSecurity.AssertDeletionAllowedAsync(userId, command.PostId);
            await this.deletePost.ExecuteAsync(command.PostId);
            await this.scheduleGarbageCollection.ExecuteAsync();
        }
    }
}