namespace Fifthweek.Api.Blogs
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Transactions;

    using Dapper;

    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;

    [AutoConstructor]
    public partial class UpdateFreeAccessUsersDbStatement : IUpdateFreeAccessUsersDbStatement
    {
        private static readonly string DeleteQuery = string.Format(
            "DELETE FROM {0} WHERE {1}=@BlogId",
            FreeAccessUser.Table,
            FreeAccessUser.Fields.BlogId);

        private static readonly string AddQuery = string.Format(
            @"INSERT {0}({1}, {2}) VALUES (@BlogId, @Email)",
            FreeAccessUser.Table,
            FreeAccessUser.Fields.BlogId,
            FreeAccessUser.Fields.Email);

        private readonly IFifthweekDbConnectionFactory connectionFactory;

        public async Task ExecuteAsync(BlogId blogId, IReadOnlyList<ValidEmail> emails)
        {
            blogId.AssertNotNull("blogId");

            if (emails == null)
            {
                emails = new List<ValidEmail>();
            }

            using (var connection = this.connectionFactory.CreateConnection())
            {
                if (emails.Count == 0)
                {
                    await connection.ExecuteAsync(
                        DeleteQuery, 
                        new { BlogId = blogId.Value });
                }
                else
                {
                    using (var transaction = TransactionScopeBuilder.CreateAsync())
                    {
                        await connection.ExecuteAsync(
                            DeleteQuery, 
                            new { BlogId = blogId.Value });

                        await connection.ExecuteAsync(
                            AddQuery,
                            emails.Select(v => new { BlogId = blogId.Value, Email = v.Value }).ToList());

                        transaction.Complete();
                    }
                }
            }
        }
    }
}