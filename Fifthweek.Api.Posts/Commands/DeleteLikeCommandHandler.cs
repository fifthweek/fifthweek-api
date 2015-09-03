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
        private readonly IPostSecurity postSecurity;
        private readonly IUnlikePostDbStatement unlikePost;

        public async Task HandleAsync(DeleteLikeCommand command)
        {
            command.AssertNotNull("command");

            var authenticatedUserId = await this.requesterSecurity.AuthenticateAsync(command.Requester);
            await this.postSecurity.AssertCommentOrLikeAllowedAsync(authenticatedUserId, command.PostId);

            await this.unlikePost.ExecuteAsync(authenticatedUserId, command.PostId);
        }
    }
}