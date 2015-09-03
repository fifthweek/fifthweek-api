namespace Fifthweek.Api.Posts
{
    using System.Threading.Tasks;

    using Dapper;

    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;

    [AutoConstructor]
    public partial class UnlikePostDbStatement : IUnlikePostDbStatement
    {
        private static readonly string Sql = string.Format(
            @"DELETE FROM {0} WHERE {1}=@UserId AND {2}=@PostId",
            Like.Table,
            Like.Fields.UserId,
            Like.Fields.PostId);

        private readonly IFifthweekDbConnectionFactory connectionFactory;

        public async Task ExecuteAsync(UserId userId, PostId postId)
        {
            userId.AssertNotNull("userId");
            postId.AssertNotNull("postId");

            using (var connection = this.connectionFactory.CreateConnection())
            {
                await connection.ExecuteAsync(
                    Sql,
                    new
                    {
                        UserId = userId.Value,
                        PostId = postId.Value,
                });
            }
        }
    }
}