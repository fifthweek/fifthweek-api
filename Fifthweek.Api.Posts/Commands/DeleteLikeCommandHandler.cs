namespace Fifthweek.Api.Posts.Commands
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;

    [AutoConstructor]
    public partial class DeleteLikeCommandHandler : ICommandHandler<DeleteLikeCommand>
    {
        private readonly IRequesterSecurity requesterSecurity;
        private readonly IUnlikePostDbStatement unlikePost;

        public async Task HandleAsync(DeleteLikeCommand command)
        {
            command.AssertNotNull("command");

            var authenticatedUserId = await this.requesterSecurity.AuthenticateAsync(command.Requester);

            // We don't do any post security checks as you should always be able to unlike a post you've previously liked.
            await this.unlikePost.ExecuteAsync(authenticatedUserId, command.PostId);
        }
    }
}