namespace Fifthweek.Api.Posts
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Subscriptions;
    using Fifthweek.CodeGeneration;

    [AutoConstructor]
    public partial class PostToCollectionDbStatement : IPostToCollectionDbStatement
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

        private readonly IFifthweekDbContext databaseContext;

        public Task ExecuteAsync(
            PostId newPostId,
            CollectionId collectionId,
            ValidComment comment,
            DateTime? sheduledPostDate,
            bool isQueued,
            FileId fileId,
            bool isFileImage)
        {
            newPostId.AssertNotNull("newPostId");
            collectionId.AssertNotNull("collectionId");
            comment.AssertNotNull("comment");
            fileId.AssertNotNull("fileId");

            var now = DateTime.UtcNow;
            var post = new Post(
                newPostId.Value,
                default(Guid), // Channel ID assigned with SQL.
                null,
                collectionId.Value,
                null,
                !isFileImage ? fileId.Value : (Guid?)null,
                null,
                isFileImage ? fileId.Value : (Guid?)null,
                null,
                comment == null ? null : comment.Value,
                null, // QueuePosition assigned with SQL (if applicable).
                null,
                now);

            if (isQueued)
            {
                return this.QueuePostAsync(post);
            }
            
            if (sheduledPostDate.HasValue)
            {
                return this.SchedulePostAsync(post, sheduledPostDate.Value, now);
            }

            return this.PostNowAsync(post, now);
        }

        protected virtual Task QueuePostAsync(Post post)
        {
            return this.databaseContext.Database.Connection.InsertAsync(
                post,
                SelectChannelId + Environment.NewLine + SelectMaxQueuePosition,
                Post.Fields.ChannelId | Post.Fields.QueuePosition);
        }

        protected virtual Task SchedulePostAsync(Post post, DateTime scheduledPostDate, DateTime now)
        {
            post.LiveDate = scheduledPostDate > now ? scheduledPostDate : now;

            return this.databaseContext.Database.Connection.InsertAsync(
                post,
                SelectChannelId,
                Post.Fields.ChannelId);
        }

        protected virtual Task PostNowAsync(Post post, DateTime now)
        {
            post.LiveDate = now;

            return this.databaseContext.Database.Connection.InsertAsync(
                post,
                SelectChannelId,
                Post.Fields.ChannelId);
        }
    }
}