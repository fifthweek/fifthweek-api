namespace Fifthweek.Api.Posts.Commands
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.CodeGeneration;

    [AutoConstructor]
    public partial class ReviseNoteCommandHandler : ICommandHandler<ReviseNoteCommand>
    {
        private readonly IRequesterSecurity requesterSecurity;
        private readonly IChannelSecurity channelSecurity;
        private readonly IPostSecurity postSecurity;
        private readonly IUpsertNoteDbStatement upsertNote;

        public async Task HandleAsync(ReviseNoteCommand command)
        {
            command.AssertNotNull("command");

            var authenticatedUserId = await this.requesterSecurity.AuthenticateAsync(command.Requester);

            await this.postSecurity.AssertWriteAllowedAsync(authenticatedUserId, command.PostId);
            await this.channelSecurity.AssertWriteAllowedAsync(authenticatedUserId, command.ChannelId);

            await this.upsertNote.ExecuteAsync(
                command.PostId,
                command.ChannelId,
                command.Note,
                command.ScheduledPostDate,
                DateTime.UtcNow);
        }
    }
}