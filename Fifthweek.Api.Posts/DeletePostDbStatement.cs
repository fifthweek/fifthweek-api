namespace Fifthweek.Api.Posts
{
    using System.Threading.Tasks;

    using Dapper;

    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;

    [AutoConstructor]
    public partial class DeletePostDbStatement : IDeletePostDbStatement
    {
        private readonly IFifthweekDbContext fifthweekDbContext;

        public Task ExecuteAsync(Shared.PostId postId)
        {
            return this.fifthweekDbContext.Database.Connection.ExecuteAsync(
                string.Format(@"DELETE FROM {0} WHERE {1}=@Id", Post.Table, Post.Fields.Id),
                new { Id = postId.Value });
        }
    }
}