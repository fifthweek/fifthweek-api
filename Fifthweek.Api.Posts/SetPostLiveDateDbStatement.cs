namespace Fifthweek.Api.Posts
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;

    [AutoConstructor]
    public partial class SetPostLiveDateDbStatement : ISetPostLiveDateDbStatement
    {
        private readonly IScheduledDateClippingFunction scheduledDateClipping;
        private readonly IFifthweekDbConnectionFactory connectionFactory;

        public async Task ExecuteAsync(PostId postId, DateTime newTime, DateTime now)
        {
            postId.AssertNotNull("postId");
            newTime.AssertUtc("newTime");
            now.AssertUtc("now");

            var post = new Post(postId.Value)
            {
                LiveDate = this.scheduledDateClipping.Apply(now, newTime),
                QueueId = null
            };

            var parameters = new SqlGenerationParameters<Post, Post.Fields>(post)
            {
                UpdateMask = Post.Fields.LiveDate | Post.Fields.QueueId
            };

            using (var connection = this.connectionFactory.CreateConnection())
            {
                await connection.UpdateAsync(parameters);
            }
        }
    }
}