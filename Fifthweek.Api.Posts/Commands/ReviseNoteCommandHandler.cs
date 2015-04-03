namespace Fifthweek.Api.Posts.Commands
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;

    [AutoConstructor]
    public partial class ReviseNoteCommandHandler : ICommandHandler<ReviseNoteCommand>
    {
        private readonly IRequesterSecurity requesterSecurity;
        private readonly IChannelSecurity channelSecurity;
        private readonly IPostSecurity postSecurity;
        private readonly IFifthweekDbConnectionFactory connectionFactory;

        public async Task HandleAsync(ReviseNoteCommand command)
        {
            command.AssertNotNull("command");

            var authenticatedUserId = await this.requesterSecurity.AuthenticateAsync(command.Requester);

            await this.postSecurity.AssertWriteAllowedAsync(authenticatedUserId, command.PostId);
            await this.channelSecurity.AssertWriteAllowedAsync(authenticatedUserId, command.ChannelId);

            await this.ReviseNoteAsync(command);
        }

        private async Task ReviseNoteAsync(ReviseNoteCommand command)
        {
            var post = new Post(command.PostId.Value)
            {
                ChannelId = command.ChannelId.Value,
                Comment = command.Note.Value
            };

            using (var connection = this.connectionFactory.CreateConnection())
            {
                await connection.UpdateAsync(post, Post.Fields.ChannelId | Post.Fields.Comment);
            }
        }
    }
}