namespace Fifthweek.Api.Blogs
{
    using System.Threading.Tasks;

    using Dapper;

    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;

    [AutoConstructor]
    public partial class DoesUserHaveFreeAccessDbStatement : IDoesUserHaveFreeAccessDbStatement
    {
        private static readonly string Query = string.Format(
            @"SELECT 
                CASE WHEN EXISTS(SELECT * FROM {0} LEFT OUTER JOIN {1} ON {0}.Email = {1}.Email WHERE {0}.{2}=@BlogId AND {1}.{3}=@UserId)
                    THEN 1 
                    ELSE 0 
                END",
            FreeAccessUser.Table,
            FifthweekUser.Table,
            FreeAccessUser.Fields.BlogId,
            FifthweekUser.Fields.Id);

        private readonly IFifthweekDbConnectionFactory connectionFactory;

        public async Task<bool> ExecuteAsync(BlogId blogId, UserId userId)
        {
            blogId.AssertNotNull("blogId");
            userId.AssertNotNull("userId");

            using (var connection = this.connectionFactory.CreateConnection())
            {
                var result = await connection.ExecuteScalarAsync<bool>(
                    Query,
                    new { BlogId = blogId.Value, UserId = userId.Value });

                return result;
            }
        }
    }
}