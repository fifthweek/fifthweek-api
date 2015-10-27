namespace Fifthweek.Api.Posts.Commands
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;

    [AutoConstructor]
    public partial class CommentOnPostCommandHandler : ICommandHandler<CommentOnPostCommand>
    {
        private readonly IRequesterSecurity requesterSecurity;
        private readonly IPostSecurity postSecurity;
        private readonly ICommentOnPostDbStatement commentOnPost;

        public async Task HandleAsync(CommentOnPostCommand command)
        {
            command.AssertNotNull("command");

            var authenticatedUserId = await this.requesterSecurity.AuthenticateAsync(command.Requester);
            await this.postSecurity.AssertCommentOrLikeAllowedAsync(authenticatedUserId, command.PostId, command.Timestamp);

            await this.commentOnPost.ExecuteAsync(
                    authenticatedUserId,
                    command.PostId,
                    command.CommentId,
                    command.Content,
                    command.Timestamp);
        }
    }
}