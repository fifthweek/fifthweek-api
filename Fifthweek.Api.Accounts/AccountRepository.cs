namespace Fifthweek.Api.Accounts
{
    using System;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Dapper;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement;
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;

    [AutoConstructor]
    public partial class AccountRepository : IAccountRepository
    {
        private readonly IFifthweekDbContext databaseContext;

        private readonly IUserManager userManager;

        public async Task<GetAccountSettingsResult> GetAccountSettingsAsync(UserId userId)
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

        public async Task<UpdateAccountSettingsResult> UpdateAccountSettingsAsync(
            UserId userId,
            ValidUsername newUsername,
            ValidEmail newEmail,
            ValidPassword newPassword,
            FileId newProfileImageFileId)
        {
            userId.AssertNotNull("userId");
            newUsername.AssertNotNull("newUsername");
            newEmail.AssertNotNull("newEmail");

            string passwordHash = null;
            if (newPassword != null)
            {
                passwordHash = this.userManager.PasswordHasher.HashPassword(newPassword.Value);
            }

            var query = new StringBuilder();

            query.Append(@"DECLARE @oldEmail varchar(").Append(ValidEmail.MaxLength).Append(@")").AppendLine();

            query.Append(@"UPDATE dbo.AspNetUsers SET @oldEmail=Email, Email=@Email, UserName=@Username, ProfileImageFileId=@ProfileImageFileId");

            if (passwordHash != null)
            {
                query.Append(@", PasswordHash=@PasswordHash");
            }

            query.AppendLine().Append(@"WHERE Id=@UserId").AppendLine();

            query.Append(@"select @oldEmail");

            var oldEmail = await this.databaseContext.Database.Connection.ExecuteScalarAsync<string>(
                query.ToString(),
                new 
                {
                    UserId = userId.Value, 
                    Username = newUsername.Value, 
                    Email = newEmail.Value,
                    PasswordHash = passwordHash,
                    ProfileImageFileId = newProfileImageFileId == null ? (Guid?)null : newProfileImageFileId.Value
                });

            return new UpdateAccountSettingsResult(oldEmail != newEmail.Value);
        }

        public class UpdateAccountSettingsResult
        {
            public UpdateAccountSettingsResult(bool emailChanged)
            {
                this.EmailChanged = emailChanged;
            }

            public bool EmailChanged { get; private set; }
        }
    }
}