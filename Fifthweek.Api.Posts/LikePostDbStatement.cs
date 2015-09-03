namespace Fifthweek.Api.Posts
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;

    [AutoConstructor]
    public partial class LikePostDbStatement : ILikePostDbStatement
    {
        private readonly IFifthweekDbConnectionFactory connectionFactory;

        public async Task ExecuteAsync(UserId userId, PostId postId, DateTime timestamp)
        {
            userId.AssertNotNull("userId");
            postId.AssertNotNull("postId");

            var like = new Like(postId.Value, null, userId.Value, null, timestamp);
            using (var connection = this.connectionFactory.CreateConnection())
            {
                await connection.UpsertAsync(
                    like,
                    Like.Fields.CreationDate);
            }
        }
    }
}