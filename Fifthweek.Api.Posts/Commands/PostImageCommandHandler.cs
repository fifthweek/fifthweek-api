namespace Fifthweek.Api.Posts.Commands
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Subscriptions;
    using Fifthweek.CodeGeneration;

    [AutoConstructor]
    public partial class PostImageCommandHandler : ICommandHandler<PostImageCommand>
    {
        private static readonly string SelectChannelId = string.Format(
            @"DECLARE @{0} uniqueidentifier = (
            SELECT {0}
            FROM   Collections 
            WHERE  {1} = @{2})",
            Collection.Fields.ChannelId,
            Collection.Fields.Id,
            Post.Fields.CollectionId);

        private static readonly string SelectMaxQueuePosition = string.Format(
            @"DECLARE @{0} int = (            
            SELECT  MAX({0})
            FROM    Posts
            WHERE   {1} = @{1})",
            Post.Fields.QueuePosition,
            Post.Fields.CollectionId);

        private readonly ICollectionSecurity collectionSecurity;
        private readonly IFifthweekDbContext databaseContext;

        public async Task HandleAsync(PostImageCommand command)
        {
            command.AssertNotNull("command");

            await this.collectionSecurity.AssertPostingAllowedAsync(command.Requester, command.CollectionId);

            if (command.IsQueued)
            {
                await this.QueuePostAsync(command);
            }
            else if (command.ScheduledPostDate != null)
            {
                await this.SchedulePostAsync(command, command.ScheduledPostDate.Value);
            }
            else
            {
                await this.PostNowAsync(command);
            }
        }

        protected virtual Task QueuePostAsync(PostImageCommand command)
        {
            return this.databaseContext.Database.Connection.InsertAsync(
                Entity(command, DateTime.UtcNow, null), 
                SelectChannelId + " " + SelectMaxQueuePosition, 
                Post.Fields.ChannelId | Post.Fields.QueuePosition);
        }

        protected virtual Task SchedulePostAsync(PostImageCommand command, DateTime scheduledPostDate)
        {
            var now = DateTime.UtcNow;
            DateTime? liveDate = now;

            if (scheduledPostDate > now)
            {
                liveDate = scheduledPostDate;
            }

            return this.databaseContext.Database.Connection.InsertAsync(
                Entity(command, now, liveDate), 
                SelectChannelId, 
                Post.Fields.ChannelId);
        }

        protected virtual Task PostNowAsync(PostImageCommand command)
        {
            var now = DateTime.UtcNow;
            return this.databaseContext.Database.Connection.InsertAsync(
                Entity(command, now, now),
                SelectChannelId,
                Post.Fields.ChannelId);
        }

        private static Post Entity(PostImageCommand command, DateTime now, DateTime? liveDate)
        {
            return new Post(
                command.NewPostId.Value,
                default(Guid),
                null,
                command.CollectionId.Value,
                null,
                null,
                null,
                command.ImageFileId.Value,
                null,
                command.Comment == null ? null : command.Comment.Value,
                null,
                liveDate,
                now);
        }
    }
}