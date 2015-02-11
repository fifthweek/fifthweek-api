namespace Fifthweek.Api.Posts
{
    using System.Threading.Tasks;

    using Dapper;

    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;

    [AutoConstructor]
    public partial class DeletePostDbStatement : IDeletePostDbStatement
    {
        private readonly IFifthweekDbConnectionFactory connectionFactory;

        public async Task ExecuteAsync(Shared.PostId postId)
        {
            using (var connection = this.connectionFactory.CreateConnection())
            {
                await connection.ExecuteAsync(
                        string.Format(@"DELETE FROM {0} WHERE {1}=@Id", Post.Table, Post.Fields.Id),
                        new { Id = postId.Value });
            }
        }
    }
}