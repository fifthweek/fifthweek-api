namespace Fifthweek.Api.Posts.Commands
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;

    [AutoConstructor]
    public partial class LikePostCommandHandler : ICommandHandler<LikePostCommand>
    {
        private readonly IRequesterSecurity requesterSecurity;
        private readonly IPostSecurity postSecurity;
        private readonly ILikePostDbStatement likePost;

        public async Task HandleAsync(LikePostCommand command)
        {
            command.AssertNotNull("command");

            var authenticatedUserId = await this.requesterSecurity.AuthenticateAsync(command.Requester);
            await this.postSecurity.AssertCommentOrLikeAllowedAsync(authenticatedUserId, command.PostId, command.Timestamp);

            await this.likePost.ExecuteAsync(authenticatedUserId, command.PostId, command.Timestamp);
        }
    }
}