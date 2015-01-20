namespace Fifthweek.Api.Posts.Commands
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Azure;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.CodeGeneration;

    [AutoConstructor]
    public partial class DeletePostCommandHandler : ICommandHandler<DeletePostCommand>
    {
        private readonly IQueueService queueService;

        private readonly IPostSecurity postSecurity;

        public async Task HandleAsync(DeletePostCommand command)
        {
            command.AssertNotNull("command");

            UserId userId;
            command.Requester.AssertAuthenticated(out userId);

            await this.postSecurity.AssertDeletionAllowedAsync(userId, command.PostId);

            // Post delete message to queue.
            
            // Delete post from table.
        }
    }
}