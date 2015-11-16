namespace Fifthweek.Api.Posts
{
    using System.Threading.Tasks;

    using Dapper;

    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;

    [AutoConstructor]
    public partial class IsFreePostDbStatement : IIsFreePostDbStatement
    {
        private static readonly string Sql = string.Format(
            @"IF EXISTS
            (
                SELECT * FROM {0} fp
                WHERE fp.{1} = @PostId AND fp.{2} = @UserId
            )
                SELECT 1 AS FOUND
            ELSE
                SELECT 0 AS FOUND",
            FreePost.Table,
            FreePost.Fields.PostId,
            FreePost.Fields.UserId);

        private readonly IFifthweekDbConnectionFactory connectionFactory;

        public async Task<bool> ExecuteAsync(UserId userId, Shared.PostId postId)
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
                        UserId = userId.Value
                    });
            }
        }
    }
}