namespace Fifthweek.Api.Posts.Commands
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.CodeGeneration;

    [AutoConstructor]
    public partial class ReviseNoteCommandHandler : ICommandHandler<ReviseNoteCommand>
    {
        private readonly IChannelSecurity channelSecurity;
        private readonly IPostSecurity postSecurity;
        private readonly IRequesterSecurity requesterSecurity;
        private readonly IFifthweekDbContext databaseContext;
        private readonly IScheduledDateClippingFunction scheduledDateClipping;

        public async Task HandleAsync(ReviseNoteCommand command)
        {
            command.AssertNotNull("command");

            var authenticatedUserId = await this.requesterSecurity.AuthenticateAsync(command.Requester);

            await this.postSecurity.AssertWriteAllowedAsync(authenticatedUserId, command.PostId);
            await this.channelSecurity.AssertWriteAllowedAsync(authenticatedUserId, command.ChannelId);

            await this.ScheduleReviseAsync(command);
        }

        private Task ScheduleReviseAsync(ReviseNoteCommand command)
        {
            var now = DateTime.UtcNow;
            var scheduledDate = this.scheduledDateClipping.Apply(now, command.ScheduledPostDate);
            var post = new Post(
                command.PostId.Value,
                command.ChannelId.Value,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                command.Note.Value,
                false,
                scheduledDate,
                default(DateTime));

            var updatedFields = Post.Fields.ChannelId | Post.Fields.Comment | Post.Fields.LiveDate;

            return this.databaseContext.Database.Connection.UpdateAsync(post, updatedFields);
        }
    }
}