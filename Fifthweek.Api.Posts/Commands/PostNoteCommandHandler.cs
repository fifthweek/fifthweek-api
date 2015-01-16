namespace Fifthweek.Api.Posts.Commands
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Subscriptions;
    using Fifthweek.CodeGeneration;

    [AutoConstructor]
    public partial class PostNoteCommandHandler : ICommandHandler<PostNoteCommand>
    {
        private readonly IChannelSecurity channelSecurity;
        private readonly IFifthweekDbContext databaseContext;

        public async Task HandleAsync(PostNoteCommand command)
        {
            command.AssertNotNull("command");

            await this.channelSecurity.AssertPostingAllowedAsync(command.Requester, command.ChannelId);

            await this.SchedulePostAsync(command);
        }

        private Task SchedulePostAsync(PostNoteCommand command)
        {
            var now = DateTime.UtcNow;

            var liveDate = now;
            if (command.ScheduledPostDate != null)
            {
                var scheduledPostDate = command.ScheduledPostDate.Value;
                if (scheduledPostDate > now)
                {
                    liveDate = scheduledPostDate;    
                }
            }

            var post = new Post(
                command.NewPostId.Value,
                command.ChannelId.Value,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                command.Note.Value,
                null,
                liveDate,
                now);

            return this.databaseContext.Database.Connection.InsertAsync(post);
        }
    }
}