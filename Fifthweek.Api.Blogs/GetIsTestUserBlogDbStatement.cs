namespace Fifthweek.Api.Blogs
{
    using System.Threading.Tasks;

    using Dapper;

    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.CodeGeneration;

    [AutoConstructor]
    public partial class GetIsTestUserBlogDbStatement : IGetIsTestUserBlogDbStatement
    {
        private static readonly string Sql = string.Format(
            @"SELECT CASE WHEN EXISTS (
                SELECT * FROM {0} blogs INNER JOIN {2} r ON blogs.{1} = r.{3}
                WHERE blogs.{6}=@BlogId AND r.{4}='{5}'
              )
              THEN CAST(1 AS BIT)
              ELSE CAST(0 AS BIT) END",
            Blog.Table,
            Blog.Fields.CreatorId,
            FifthweekUserRole.Table,
            FifthweekUserRole.Fields.UserId,
            FifthweekUserRole.Fields.RoleId,
            FifthweekRole.TestUserId.ToString(),
            Blog.Fields.Id);

        private readonly IFifthweekDbConnectionFactory connectionFactory;

        public async Task<bool> ExecuteAsync(BlogId blogId)
        {
            using (var connection = this.connectionFactory.CreateConnection())
            {
                var result = await connection.ExecuteScalarAsync<bool>(
                    Sql,
                    new 
                    {
                        BlogId = blogId.Value
                    });

                return result;
            }
        }
    }
}