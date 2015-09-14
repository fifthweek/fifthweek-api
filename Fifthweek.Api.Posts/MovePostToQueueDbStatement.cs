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
    public partial class MovePostToQueueDbStatement : IMovePostToQueueDbStatement
    {
        public static readonly string WherePostLiveDateUniqueToQueue = string.Format(
            @"
            NOT EXISTS (SELECT * 
                        FROM  {0} WITH (UPDLOCK, HOLDLOCK)
                        WHERE {1} != @{1}
                        AND   {2}  = @{2}
                        AND   {3}  = @{3})",
            Post.Table,
            Post.Fields.Id,
            Post.Fields.QueueId,
            Post.Fields.LiveDate);

        private static readonly string WherePostNotInQueue = string.Format(
            @"
            NOT EXISTS (SELECT * 
                    FROM    {0} WITH (UPDLOCK, HOLDLOCK)
                    WHERE   {1} = @{1}
                    AND     {2} = @{2})",
            Post.Table,
            Post.Fields.Id,
            Post.Fields.QueueId);

        private readonly IFifthweekDbConnectionFactory connectionFactory;
        private readonly IGetLiveDateOfNewQueuedPostDbStatement getLiveDateOfNewQueuedPost;

        public async Task ExecuteAsync(PostId postId, QueueId queueId)
        {
            postId.AssertNotNull("postId");
            queueId.AssertNotNull("queueId");

            var nextLiveDate = await this.getLiveDateOfNewQueuedPost.ExecuteAsync(queueId);

            var post = new Post(postId.Value)
            {
                QueueId = queueId.Value,
                LiveDate = nextLiveDate
            };

            var parameters = new SqlGenerationParameters<Post, Post.Fields>(post)
            {
                UpdateMask = Post.Fields.QueueId | Post.Fields.LiveDate,
                Conditions = new[]
                {
                    WherePostLiveDateUniqueToQueue, // Perform locks in 'descending supersets' to avoid deadlock.
                    WherePostNotInQueue
                }
            };

            using (var connection = this.connectionFactory.CreateConnection())
            {
                var failedConditionIndex = await connection.UpdateAsync(parameters);
                var concurrencyFailure = failedConditionIndex == 0;

                if (concurrencyFailure)
                {
                    throw new OptimisticConcurrencyException(string.Format("Failed to optimistically queue post with queue {0}", queueId));
                }
            }
        }
    }
}