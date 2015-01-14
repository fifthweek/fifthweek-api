namespace Fifthweek.Api.FileManagement
{
    using System;
    using System.Threading.Tasks;

    using Dapper;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.Api.Persistence;

    [AutoConstructor]
    public partial class FileSecurity : IFileSecurity
    {
        private readonly IFifthweekDbContext fifthweekDbContext;

        public async Task AssertFileBelongsToUserAsync(UserId userId, FileId fileId)
        {
            var fileBelongsToUser = await this.CheckFileBelongsToUserAsync(userId, fileId);

            if (!fileBelongsToUser)
            {
                throw new UnauthorizedException("The user " + userId.Value + " does not have permission to access file " + fileId.Value);
            }
        }

        public Task<bool> CheckFileBelongsToUserAsync(UserId userId, FileId fileId)
        {
            if (userId == null)
            {
                throw new ArgumentNullException("userId");
            }

            if (fileId == null)
            {
                throw new ArgumentNullException("fileId");
            }

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