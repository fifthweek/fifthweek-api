namespace Fifthweek.Api.FileManagement
{
    using System.Threading.Tasks;

    using Dapper;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;


    [AutoConstructor]
    public partial class FileOwnership : IFileOwnership
    {
        private readonly IFifthweekDbContext fifthweekDbContext;

        public Task<bool> IsOwnerAsync(UserId userId, Shared.FileId fileId)
        {
            userId.AssertNotNull("userId");
            fileId.AssertNotNull("fileId");

            return this.fifthweekDbContext.Database.Connection.ExecuteScalarAsync<bool>(
                @"IF EXISTS(SELECT *
                            FROM   Files
                            WHERE  Id = @FileId
                            AND    UserId = @UserId)
                    SELECT 1 AS FOUND
                ELSE
                    SELECT 0 AS FOUND",
                new
                {
                    FileId = fileId.Value,
                    UserId = userId.Value
                });
        }
    }
}