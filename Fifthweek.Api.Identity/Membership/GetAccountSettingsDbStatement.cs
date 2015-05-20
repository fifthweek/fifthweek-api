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
    using Fifthweek.Shared;

    [AutoConstructor]
    public partial class GetAccountSettingsDbStatement : IGetAccountSettingsDbStatement
    {
        private readonly IFifthweekDbConnectionFactory connectionFactory;

        public async Task<GetAccountSettingsDbResult> ExecuteAsync(UserId userId)
        {
            userId.AssertNotNull("userId");

            using (var connection = this.connectionFactory.CreateConnection())
            {
                var result = (await connection.QueryAsync<GetAccountSettingsDapperResult>(
                         @"SELECT Name, UserName, Email, ProfileImageFileId FROM dbo.AspNetUsers WHERE Id=@UserId",
                         new { UserId = userId.Value })).SingleOrDefault();

                if (result == null)
                {
                    throw new DetailedRecoverableException(
                        "Unknown user.",
                        "The user ID " + userId.Value + " was not found in the database.");
                }

                return new GetAccountSettingsDbResult(
                    result.Name == null ? null : new CreatorName(result.Name),
                    new Username(result.UserName),
                    new Email(result.Email), 
                    result.ProfileImageFileId == null ? null : new FileId(result.ProfileImageFileId.Value));
            }
        }

        private class GetAccountSettingsDapperResult
        {
            public string Name { get; set; }

            public string UserName { get; set; }

            public string Email { get; set; }

            public Guid? ProfileImageFileId { get; set; }
        }
    }

    [AutoConstructor]
    [AutoEqualityMembers]
    public partial class GetAccountSettingsDbResult
    {
        [Optional]
        public CreatorName Name { get; private set; }

        public Username Username { get; private set; }

        public Email Email { get; private set; }

        [Optional]
        public FileId ProfileImageFileId { get; private set; }
    }
}