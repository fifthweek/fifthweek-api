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
    public partial class PostToChannelDbSubStatements : IPostToChannelDbSubStatements
    {
        public static readonly string WherePostLiveDateUniqueToQueue = string.Format(
            @"NOT EXISTS (SELECT * 
                          FROM  {0} WITH (UPDLOCK, HOLDLOCK)
                          WHERE {1} = @{1}
                          AND   {2} = @{2})",
            Post.Table,
            Post.Fields.QueueId,
            Post.Fields.LiveDate); 

        private readonly IFifthweekDbConnectionFactory connectionFactory;
        private readonly IGetLiveDateOfNewQueuedPostDbStatement getLiveDateOfNewQueuedPost;
        private readonly IScheduledDateClippingFunction scheduledDateClipping;

        public async Task QueuePostAsync(Post post)
        {
            post.AssertNotNull("post");

            if (!post.QueueId.HasValue)
            {
                throw new ArgumentException("Queue ID required", "post");
            }

            var queueId = new QueueId(post.QueueId.Value);
            var nextLiveDate = await this.getLiveDateOfNewQueuedPost.ExecuteAsync(queueId);
            post.LiveDate = nextLiveDate;
            
            var parameters = new SqlGenerationParameters<Post, Post.Fields>(post)
            {
                Conditions = new[] { WherePostLiveDateUniqueToQueue }
            };

            using (var connection = this.connectionFactory.CreateConnection())
            {
                var success = -1 == await connection.InsertAsync(parameters);

                if (!success)
                {
                    throw new OptimisticConcurrencyException(string.Format("Failed to optimistically queue post to channel {0}", post.ChannelId));
                }
            }
        }

        public async Task SchedulePostAsync(Post post, DateTime? scheduledPostDate, DateTime now)
        {
            post.AssertNotNull("post");

            if (post.QueueId.HasValue)
            {
                throw new ArgumentException("Queue ID should not exist", "post");
            }

            if (scheduledPostDate.HasValue)
            {
                now.AssertUtc("scheduledPostDate");    
            }
            
            now.AssertUtc("now");

            post.LiveDate = this.scheduledDateClipping.Apply(now, scheduledPostDate);

            using (var connection = this.connectionFactory.CreateConnection())
            {
                await connection.InsertAsync(post);
            }
        }
    }
}