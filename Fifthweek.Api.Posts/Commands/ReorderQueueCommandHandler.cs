namespace Fifthweek.Api.Posts.Commands
{
    using System;
    using System.Text;
    using System.Threading.Tasks;

    using Dapper;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;

    [AutoConstructor]
    public partial class ReorderQueueCommandHandler : ICommandHandler<ReorderQueueCommand>
    {
        private const string SqlPreInserts = 
            @"
            -- Posts requested to be updated.
            CREATE TABLE #RequestedPostsToReorder(Id uniqueidentifier, Position int);";

        private static readonly string SqlPostInserts = string.Format(
            @"
            -- Posts allowed to be updated.
            CREATE TABLE #PostsToReorder(Id uniqueidentifier, LiveDate datetime, Position int);
            INSERT INTO #PostsToReorder 
            SELECT      post.{1}, post.{2}, requestedPostToReorder.Position
            FROM        #RequestedPostsToReorder requestedPostToReorder
            INNER JOIN  {0} post
              ON        requestedPostToReorder.Id = post.{1}
            INNER JOIN  {6} channel
              ON        post.{4} = channel.{7}
            INNER JOIN  {9} subscription
              ON        channel.{8} = subscription.{10}
            WHERE       subscription.{11} = @CreatorId
            AND         post.{5}          = @CollectionId
            AND         post.{2}          > @Now
            AND         post.{3}          = 1;

            -- Posts ordered by LiveDate with incremental index.
            CREATE TABLE #LiveDateOrdering(Id uniqueidentifier, LiveDate datetime, [Index] int);
            INSERT INTO #LiveDateOrdering 
            SELECT      Id, LiveDate, ROW_NUMBER() OVER (ORDER BY LiveDate DESC)
            FROM        #PostsToReorder;

            -- Posts ordered by Position with incremental index.
            CREATE TABLE #PositionOrdering(Id uniqueidentifier, [Index] int);
            INSERT INTO #PositionOrdering 
            SELECT      Id, ROW_NUMBER() OVER (ORDER BY Position)
            FROM        #PostsToReorder;

            -- Merge LiveDate descending with Position ascending.
            CREATE TABLE #ReorderedPosts(Id uniqueidentifier, LiveDate datetime);
            INSERT INTO #ReorderedPosts 
            SELECT      postByPosition.Id, postByLiveDate.LiveDate -- Re-pair the Id and LiveDate from the merged tables
            FROM        #LiveDateOrdering postByLiveDate
            INNER JOIN  #PositionOrdering postByPosition
              ON        postByLiveDate.[Index] = postByPosition.[Index];

            -- Update Posts.LiveDate so they match ReorderedPosts.
            UPDATE      {0} 
            SET         {2} = reorderedPost.LiveDate
            FROM        {0} post
            INNER JOIN  #ReorderedPosts reorderedPost
              ON        post.{1} = reorderedPost.Id;",
            Post.Table,
            Post.Fields.Id,
            Post.Fields.LiveDate,
            Post.Fields.ScheduledByQueue,
            Post.Fields.ChannelId,
            Post.Fields.CollectionId,
            Channel.Table,
            Channel.Fields.Id,
            Channel.Fields.SubscriptionId,
            Subscription.Table,
            Subscription.Fields.Id,
            Subscription.Fields.CreatorId);

        private readonly IRequesterSecurity requesterSecurity;
        private readonly IFifthweekDbContext databaseContext;

        public async Task HandleAsync(ReorderQueueCommand command)
        {
            command.AssertNotNull("command");

            var userId = await this.requesterSecurity.AuthenticateAsync(command.Requester);

            await this.ReorderQueueAsync(command, userId);
        }

        private async Task ReorderQueueAsync(ReorderQueueCommand command, UserId userId)
        {
            if (command.NewPartialQueueOrder.Count < 2)
            {
                // At least 2 posts are required for a reorder.
                return;
            }

            var sql = new StringBuilder();
            sql.AppendLine(SqlPreInserts);
            for (var i = 0; i < command.NewPartialQueueOrder.Count; i++)
            {
                var postId = command.NewPartialQueueOrder[i];
                sql.Append("INSERT INTO #RequestedPostsToReorder VALUES ('");
                sql.Append(postId.Value.ToString("D"));
                sql.Append("', ");
                sql.Append(i);
                sql.AppendLine(");");
            }

            sql.AppendLine(SqlPostInserts);

            var parameters = new
            {
                CreatorId = userId.Value,
                CollectionId = command.CollectionId.Value,
                Now = DateTime.UtcNow
            };

            await this.databaseContext.Database.Connection.ExecuteAsync(sql.ToString(), parameters);
        }
    }
}