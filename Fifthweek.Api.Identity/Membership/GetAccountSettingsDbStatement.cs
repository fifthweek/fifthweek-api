namespace Fifthweek.Api.Identity.Membership
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Dapper;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;

    [AutoConstructor]
    public partial class GetAccountSettingsDbStatement : IGetAccountSettingsDbStatement
    {
        private readonly IFifthweekDbConnectionFactory connectionFactory;

        public async Task<GetAccountSettingsResult> ExecuteAsync(UserId userId)
        {
            userId.AssertNotNull("userId");

            using (var connection = this.connectionFactory.CreateConnection())
            {
                var result = (await connection.QueryAsync<GetAccountSettingsDapperResult>(
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
        }

        private class GetAccountSettingsDapperResult
        {
            public string Email { get; set; }

            public Guid ProfileImageFileId { get; set; }
        }
    }
}