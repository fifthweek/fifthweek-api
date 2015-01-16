namespace Fifthweek.Api.Posts
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;

    [AutoConstructor]
    public partial class PostToCollectionDbSubStatements : IPostToCollectionDbSubStatements
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

        public Task QueuePostAsync(Post post)
        {
            return this.databaseContext.Database.Connection.InsertAsync(
                post,
                SelectChannelId + Environment.NewLine + SelectMaxQueuePosition,
                Post.Fields.ChannelId | Post.Fields.QueuePosition);
        }

        public Task SchedulePostAsync(Post post, DateTime scheduledPostDate, DateTime now)
        {
            post.LiveDate = scheduledPostDate > now ? scheduledPostDate : now;

            return this.databaseContext.Database.Connection.InsertAsync(
                post,
                SelectChannelId,
                Post.Fields.ChannelId);
        }

        public Task PostNowAsync(Post post, DateTime now)
        {
            post.LiveDate = now;

            return this.databaseContext.Database.Connection.InsertAsync(
                post,
                SelectChannelId,
                Post.Fields.ChannelId);
        }
    }
}