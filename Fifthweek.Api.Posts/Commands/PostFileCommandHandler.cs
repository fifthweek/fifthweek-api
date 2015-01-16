namespace Fifthweek.Api.Posts.Commands
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.Subscriptions;
    using Fifthweek.CodeGeneration;

    [AutoConstructor]
    public partial class PostFileCommandHandler : ICommandHandler<PostFileCommand>
    {
        private readonly ICollectionSecurity collectionSecurity;
        private readonly IPostToCollectionDbStatement postToCollectionDbStatement;

        public async Task HandleAsync(PostFileCommand command)
        {
            command.AssertNotNull("command");

            await this.collectionSecurity.AssertPostingAllowedAsync(command.Requester, command.CollectionId);

            await this.postToCollectionDbStatement.ExecuteAsync(
                command.NewPostId,
                command.CollectionId,
                command.Comment,
                command.ScheduledPostDate,
                command.IsQueued,
                command.FileId,
                false);
        }
    }
}