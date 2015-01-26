namespace Fifthweek.Api.Accounts
{
    using System.Threading.Tasks;

    using Fifthweek.Api.FileManagement;
    using Fifthweek.Api.Identity.Membership;

    public interface IUpdateAccountSettingsDbStatement
    {
        Task<UpdateAccountSettingsDbStatement.UpdateAccountSettingsResult> ExecuteAsync(
            UserId userId,
            ValidUsername newUsername,
            ValidEmail newEmail,
            ValidPassword newPassword,
            FileId newProfileImageFileId);
    }
}