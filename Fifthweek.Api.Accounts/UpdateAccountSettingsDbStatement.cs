namespace Fifthweek.Api.Accounts
{
    using System;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Dapper;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;

    [AutoConstructor]
    public partial class UpdateAccountSettingsDbStatement : IUpdateAccountSettingsDbStatement
    {
        private readonly IFifthweekDbConnectionFactory connectionFactory;

        private readonly IUserManager userManager;

        public async Task<UpdateAccountSettingsResult> ExecuteAsync(
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

            query.AppendLine(@"DECLARE @emailConfirmed bit");

            query.Append(@"
                UPDATE dbo.AspNetUsers 
                SET 
                    Email = @Email, 
                    UserName = @Username, 
                    ProfileImageFileId = @ProfileImageFileId,
                    @emailConfirmed =
                    (
                        CASE
                            WHEN 
                                ((EmailConfirmed = 0) OR (Email != @Email))
                            THEN
                                0
                            ELSE
                                1
                        END
                    ),
                    EmailConfirmed = @emailConfirmed");

            if (passwordHash != null)
            {
                query.Append(@", PasswordHash=@PasswordHash");
            }

            query.AppendLine().Append(@"WHERE Id=@UserId").AppendLine();

            query.Append(@"select @emailConfirmed");

            using (var connection = this.connectionFactory.CreateConnection())
            {
                var emailConfirmed = await connection.ExecuteScalarAsync<bool>(
                    query.ToString(),
                    new
                    {
                        UserId = userId.Value,
                        Username = newUsername.Value,
                        Email = newEmail.Value,
                        PasswordHash = passwordHash,
                        ProfileImageFileId = newProfileImageFileId == null ? (Guid?)null : newProfileImageFileId.Value
                    });

                return new UpdateAccountSettingsResult(emailConfirmed);
            }
        }

        [AutoConstructor, AutoEqualityMembers]
        public partial class UpdateAccountSettingsResult
        {
            public bool EmailConfirmed { get; private set; }
        }
    }
}