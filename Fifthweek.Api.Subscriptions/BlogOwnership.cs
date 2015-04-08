namespace Fifthweek.Api.Subscriptions
{
    using System;
    using System.Threading.Tasks;

    using Dapper;

    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;

    [AutoConstructor]
    public partial class BlogOwnership : IBlogOwnership
    {
        private readonly IFifthweekDbConnectionFactory connectionFactory;

        public async Task<bool> IsOwnerAsync(UserId userId, Shared.SubscriptionId blogId)
        {
            if (userId == null)
            {
                throw new ArgumentNullException("userId");
            }

            if (blogId == null)
            {
                throw new ArgumentNullException("blogId");
            }

            using (var connection = this.connectionFactory.CreateConnection())
            {
                return await connection.ExecuteScalarAsync<bool>(
                    @"IF EXISTS(SELECT *
                                FROM   Blogs
                                WHERE  Id = @BlogId
                                AND    CreatorId = @CreatorId)
                        SELECT 1 AS FOUND
                    ELSE
                        SELECT 0 AS FOUND",
                    new
                    {
                        BlogId = blogId.Value,
                        CreatorId = userId.Value
                    });
            }
        }
    }
}