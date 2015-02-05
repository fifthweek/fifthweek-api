namespace Fifthweek.Api.Posts
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;

    [AutoConstructor]
    public partial class PostToCollectionDbSubStatements : IPostToCollectionDbSubStatements
    {
        public static readonly string WherePostLiveDateUniqueToCollection = string.Format(
            @"NOT EXISTS (SELECT * 
                          FROM  {0} WITH (UPDLOCK, HOLDLOCK)
                          WHERE {1} = @{1}
                          AND   {2} = @{2}
                          AND   {3} = 1)",
            Post.Table,
            Post.Fields.CollectionId,
            Post.Fields.LiveDate,
            Post.Fields.ScheduledByQueue); 

        private static readonly string DeclareChannelId = string.Format(
            @"DECLARE @{1} uniqueidentifier = (
            SELECT {1}
            FROM   {0} 
            WHERE  {2} = @{3})",
            Collection.Table,
            Collection.Fields.ChannelId,
            Collection.Fields.Id,
            Post.Fields.CollectionId);

        private readonly IFifthweekDbContext databaseContext;
        private readonly IGetLiveDateOfNewQueuedPostDbStatement getLiveDateOfNewQueuedPost;
        private readonly IScheduledDateClippingFunction scheduledDateClipping;

        public async Task QueuePostAsync(Post unscheduledPostWithoutChannel)
        {
            unscheduledPostWithoutChannel.AssertNotNull("unscheduledPostWithoutChannel");

            if (!unscheduledPostWithoutChannel.CollectionId.HasValue)
            {
                // We only assert the existence of a collection ID in this method as it is required below.
                // This is to remove compiler warnings more than anything. In reality, all these methods hinge 
                // on the fact that the caller (which should only ever be PostToCollectionDbStatement) has 
                // provided a partially complete Post entity, where one of the populated fields is CollectionId.
                throw new ArgumentException("Collection ID required", "unscheduledPostWithoutChannel");
            }

            var collectionId = new CollectionId(unscheduledPostWithoutChannel.CollectionId.Value);
            
            var nextLiveDate = await this.getLiveDateOfNewQueuedPost.ExecuteAsync(collectionId);

            var scheduledPostWithoutChannel = unscheduledPostWithoutChannel.Copy(_ =>
            {
                _.ScheduledByQueue = true;
                _.LiveDate = nextLiveDate;
            });

            var parameters = new SqlGenerationParameters<Post, Post.Fields>(scheduledPostWithoutChannel)
            {
                Declarations = DeclareChannelId,
                ExcludedFromInput = Post.Fields.ChannelId,
                Conditions = new[] { WherePostLiveDateUniqueToCollection }
            };

            var success = -1 == await this.databaseContext.Database.Connection.InsertAsync(parameters);

            if (!success)
            {
                // Log the collection ID, as the post ID will be useless for debugging (as it was never created).
                throw new OptimisticConcurrencyException(string.Format("Failed to optimistically queue post. {0}", collectionId));
            }
        }

        public Task SchedulePostAsync(Post unscheduledPostWithoutChannel, DateTime? scheduledPostDate, DateTime now)
        {
            unscheduledPostWithoutChannel.AssertNotNull("unscheduledPostWithoutChannel");

            if (scheduledPostDate.HasValue)
            {
                now.AssertUtc("scheduledPostDate");    
            }
            
            now.AssertUtc("now");

            var scheduledPostWithoutChannel = unscheduledPostWithoutChannel.Copy(_ =>
            {
                _.ScheduledByQueue = false;
                _.LiveDate = this.scheduledDateClipping.Apply(now, scheduledPostDate);
            });

            var parameters = new SqlGenerationParameters<Post, Post.Fields>(scheduledPostWithoutChannel)
            {
                Declarations = DeclareChannelId,
                ExcludedFromInput = Post.Fields.ChannelId
            };

            return this.databaseContext.Database.Connection.InsertAsync(parameters);
        }
    }
}