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
            SELECT  ISNULL(MAX({0}), -1) + 1
            FROM    Posts
            WHERE   {1} = @{1})",
            Post.Fields.QueuePosition,
            Post.Fields.CollectionId);

        private readonly IFifthweekDbContext databaseContext;

        public Task QueuePostAsync(Post unscheduledPostWithoutChannel)
        {
            return this.databaseContext.Database.Connection.InsertAsync(
                unscheduledPostWithoutChannel,
                SelectChannelId + Environment.NewLine + SelectMaxQueuePosition,
                Post.Fields.ChannelId | Post.Fields.QueuePosition);
        }

        public Task SchedulePostAsync(Post unscheduledPostWithoutChannel, DateTime scheduledPostDate, DateTime now)
        {
            unscheduledPostWithoutChannel.LiveDate = scheduledPostDate > now ? scheduledPostDate : now;

            return this.databaseContext.Database.Connection.InsertAsync(
                unscheduledPostWithoutChannel,
                SelectChannelId,
                Post.Fields.ChannelId);
        }

        public Task PostNowAsync(Post unscheduledPostWithoutChannel, DateTime now)
        {
            unscheduledPostWithoutChannel.LiveDate = now;

            return this.databaseContext.Database.Connection.InsertAsync(
                unscheduledPostWithoutChannel,
                SelectChannelId,
                Post.Fields.ChannelId);
        }
    }
}