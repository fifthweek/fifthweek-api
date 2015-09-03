namespace Fifthweek.Api.Posts
{
    using System.Threading.Tasks;

    using Dapper;

    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;

    [AutoConstructor]
    public partial class DeletePostDbStatement : IDeletePostDbStatement
    {
        private static readonly string Sql = string.Format(
            @"
            DELETE FROM {2} WHERE {3}=@PostId
            DELETE FROM {4} WHERE {5}=@PostId
            DELETE FROM {0} WHERE {1}=@PostId",
            Post.Table,
            Post.Fields.Id,
            Like.Table,
            Like.Fields.PostId,
            Comment.Table,
            Comment.Fields.PostId);

        private readonly IFifthweekDbConnectionFactory connectionFactory;

        public async Task ExecuteAsync(Shared.PostId postId)
        {
            using (var connection = this.connectionFactory.CreateConnection())
            {
                await connection.ExecuteAsync(
                        Sql,
                        new { PostId = postId.Value });
            }
        }
    }
}