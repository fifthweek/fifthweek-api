namespace Fifthweek.Api.Posts
{
    using System;
    using System.Threading.Tasks;

    using Dapper;

    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;

    [AutoConstructor]
    public partial class IsPostSubscriberDbStatement : IIsPostSubscriberDbStatement
    {
        private static readonly string Sql = string.Format(
            @"
            {4}
            IF EXISTS
            (
                SELECT * FROM {0} post
                WHERE post.{1} = @PostId
                AND post.{2} <= @Now
                {3}
            )
                SELECT 1 AS FOUND
            ELSE
                SELECT 0 AS FOUND",
            Post.Table,
            Post.Fields.Id,
            Post.Fields.LiveDate,
            GetNewsfeedDbStatement.GetSubscriptionFilter(),
            GetNewsfeedDbStatement.GetPaymentDeclarations());

        private readonly IFifthweekDbConnectionFactory connectionFactory;

        public async Task<bool> ExecuteAsync(UserId userId, PostId postId, DateTime now)
        {
            userId.AssertNotNull("userId");
            postId.AssertNotNull("postId");

            using (var connection = this.connectionFactory.CreateConnection())
            {
                return await connection.ExecuteScalarAsync<bool>(
                    Sql,
                    new
                        {
                            PostId = postId.Value,
                            RequestorId = userId.Value,
                            Now = now
                        });
            }
        }
    }
}