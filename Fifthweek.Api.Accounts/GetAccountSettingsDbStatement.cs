namespace Fifthweek.Api.Accounts
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Dapper;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement;
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;

    using Email = Fifthweek.Api.Identity.Shared.Membership.Email;
    using FileId = Fifthweek.Api.FileManagement.Shared.FileId;
    using UserId = Fifthweek.Api.Identity.Shared.Membership.UserId;

    [AutoConstructor]
    public partial class GetAccountSettingsDbStatement : IGetAccountSettingsDbStatement
    {
        private readonly IFifthweekDbContext databaseContext;

        public async Task<GetAccountSettingsResult> ExecuteAsync(UserId userId)
        {
            userId.AssertNotNull("userId");

            var result = (await this.databaseContext.Database.Connection.QueryAsync<GetAccountSettingsDapperResult>(
                @"SELECT Email, ProfileImageFileId FROM dbo.AspNetUsers WHERE Id=@UserId",
                new { UserId = userId.Value })).SingleOrDefault();

            if (result == null)
            {
                throw new DetailedRecoverableException(
                    "Unknown user.",
                    "The user ID " + userId.Value + " was not found in the database.");
            }

            return new GetAccountSettingsResult(new Email(result.Email), new FileId(result.ProfileImageFileId));
        }

        private class GetAccountSettingsDapperResult
        {
            public string Email { get; set; }

            public Guid ProfileImageFileId { get; set; }
        }
    }
}