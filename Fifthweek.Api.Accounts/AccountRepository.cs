namespace Fifthweek.Api.Accounts
{
    using System.Text;
    using System.Threading.Tasks;

    using Dapper;

    using Fifthweek.Api.Accounts.Controllers;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement;
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.Api.Persistence;

    [AutoConstructor]
    public partial class AccountRepository : IAccountRepository
    {
        private readonly IFifthweekDbContext databaseContext;

        private readonly IUserManager userManager;

        public Task<AccountSettingsResult> GetAccountSettingsAsync(UserId userId)
        {
            return this.databaseContext.Database.Connection.ExecuteScalarAsync<AccountSettingsResult>(
                @"SELECT Email, ProfileImageFileId FROM dbo.AspNetUsers WHERE Id=@UserId",
                new { UserId = userId });
        }

        public async Task<UpdateAccountSettingsResult> UpdateAccountSettingsAsync(
            UserId userId,
            Username newUsername,
            Email newEmail,
            Password newPassword,
            FileId newProfileImageFileId)
        {
            string passwordHash = null;
            if (newPassword != null)
            {
                passwordHash = this.userManager.PasswordHasher.HashPassword(newPassword.Value);
            }

            var query = new StringBuilder();

            query.Append(@"DECLARE @oldEmail varchar(").Append(Email.MaxLength).Append(@")").AppendLine();

            query.Append(@"UPDATE dbo.AspNetUsers SET @oldEmail=Email, Email=@NewEmail, UserName=@Username, ProfileImageFileId=@ProfileImageFileId");

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
                    UserId = userId, 
                    Username = newUsername.Value, 
                    Email = newEmail.Value,
                    PasswordHash = passwordHash,
                    ProfileImageFileId = newProfileImageFileId == null ? null : newProfileImageFileId.Value.ToString()
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