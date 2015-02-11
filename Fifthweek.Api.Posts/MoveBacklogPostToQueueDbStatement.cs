namespace Fifthweek.Api.Posts
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;

    [AutoConstructor]
    public partial class MoveBacklogPostToQueueDbStatement : IMoveBacklogPostToQueueDbStatement
    {
        public static readonly string WherePostLiveDateUniqueToCollection = string.Format(
            @"
            NOT EXISTS (SELECT * 
                        FROM  {0} WITH (UPDLOCK, HOLDLOCK)
                        WHERE {1} != @{1}
                        AND   {2}  = @{2}
                        AND   {3}  = @{3}
                        AND   {4}  = 1)",
            Post.Table,
            Post.Fields.Id,
            Post.Fields.CollectionId,
            Post.Fields.LiveDate,
            Post.Fields.ScheduledByQueue); 

        private static readonly string WherePostInCollection = string.Format(
            @"
            EXISTS (SELECT * 
                    FROM    {0} WITH (UPDLOCK, HOLDLOCK)
                    WHERE   {1} = @{1}
                    AND     {2} = @CollectionId)",
            Post.Table,
            Post.Fields.Id,
            Post.Fields.CollectionId);

        private static readonly string WherePostInBacklogButNotQueued = string.Format(
            @"
            EXISTS (SELECT * 
                    FROM    {0} WITH (UPDLOCK, HOLDLOCK)
                    WHERE   {1} = @{1}
                    AND     {2} > @Now
                    AND     {3} = 0)",
            Post.Table,
            Post.Fields.Id,
            Post.Fields.LiveDate,
            Post.Fields.ScheduledByQueue);

        private readonly IFifthweekDbConnectionFactory connectionFactory;
        private readonly IGetLiveDateOfNewQueuedPostDbStatement getLiveDateOfNewQueuedPost;

        public async Task ExecuteAsync(PostId postId, CollectionId currentCollectionId, DateTime now)
        {
            postId.AssertNotNull("postId");
            currentCollectionId.AssertNotNull("currentCollectionId");
            now.AssertUtc("now");

            var nextLiveDate = await this.getLiveDateOfNewQueuedPost.ExecuteAsync(currentCollectionId);

            var post = new Post(postId.Value)
            {
                ScheduledByQueue = true,
                LiveDate = nextLiveDate
            };

            var parameters = new SqlGenerationParameters<Post, Post.Fields>(post)
            {
                UpdateMask = Post.Fields.ScheduledByQueue | Post.Fields.LiveDate,
                AdditionalParameters = new
                {
                    CollectionId = currentCollectionId.Value,
                    Now = now
                },
                Conditions = new[]
                {
                    WherePostLiveDateUniqueToCollection, // Perform locks in 'descending supersets' to avoid deadlock.
                    WherePostInCollection, // May have changed between calculation and update.
                    WherePostInBacklogButNotQueued // Make operation idempotent.
                }
            };

            using (var connection = this.connectionFactory.CreateConnection())
            {
                var failedConditionIndex = await connection.UpdateAsync(parameters);
                var concurrencyFailure = failedConditionIndex == 0 || failedConditionIndex == 1;

                if (concurrencyFailure)
                {
                    // Log the collection ID, as the post ID will be useless for debugging (as it was never created).
                    throw new OptimisticConcurrencyException(string.Format("Failed to optimistically queue post. {0}", currentCollectionId));
                }
            }
        }
    }
}