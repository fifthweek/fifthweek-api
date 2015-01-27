namespace Fifthweek.Api.Posts.Commands
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.CodeGeneration;

    [AutoConstructor]
    public partial class DeletePostCommandHandler : ICommandHandler<DeletePostCommand>
    {
        private readonly IScheduleGarbageCollectionStatement scheduleGarbageCollection;
        private readonly IPostSecurity postSecurity;
        private readonly IRequesterSecurity requesterSecurity;
        private readonly IDeletePostDbStatement deletePost;

        public async Task HandleAsync(DeletePostCommand command)
        {
            command.AssertNotNull("command");

            var userId = await this.requesterSecurity.AuthenticateAsync(command.Requester);

            await this.postSecurity.AssertDeletionAllowedAsync(userId, command.PostId);
            await this.deletePost.ExecuteAsync(command.PostId);
            await this.scheduleGarbageCollection.ExecuteAsync();
        }
    }
}