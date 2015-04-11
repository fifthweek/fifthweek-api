namespace Fifthweek.Api.Blogs
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Transactions;

    using Dapper;

    using Fifthweek.Api.Blogs.Queries;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;

    [AutoConstructor]
    public partial class GetFreeAccessUsersDbStatement : IGetFreeAccessUsersDbStatement
    {
        private static readonly string Query = string.Format(
            "SELECT {0}.{3}, {1}.{4}, {1}.{5} FROM {0} LEFT OUTER JOIN {1} ON {0}.Email = {1}.Email WHERE {0}.{2}=@BlogId",
            FreeAccessUser.Table,
            FifthweekUser.Table,
            FreeAccessUser.Fields.BlogId,
            FreeAccessUser.Fields.Email,
            FifthweekUser.Fields.Id,
            FifthweekUser.Fields.UserName);

        private readonly IFifthweekDbConnectionFactory connectionFactory;

        public async Task<GetFreeAccessUsersResult> ExecuteAsync(BlogId blogId)
        {
            blogId.AssertNotNull("blogId");

            using (var connection = this.connectionFactory.CreateConnection())
            {
                var queryResult = await connection.QueryAsync<QueryResult>(
                    Query,
                    new { BlogId = blogId.Value });

                return new GetFreeAccessUsersResult(
                    queryResult.Select(v => new GetFreeAccessUsersResult.FreeAccessUser(
                        new Email(v.Email),
                        v.Id.HasValue ? new UserId(v.Id.Value) : null,
                        v.UserName != null ? new Username(v.UserName) : null)).ToList());
            }
        }

        public class QueryResult
        {
            public string Email { get; set; }
            
            public Guid? Id { get; set; }
            
            public string UserName { get; set; }
        }
    }
}