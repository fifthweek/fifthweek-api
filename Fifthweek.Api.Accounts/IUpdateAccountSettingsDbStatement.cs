namespace Fifthweek.Api.Accounts
{
    using System.Threading.Tasks;

    using Fifthweek.Api.FileManagement;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.Api.Identity.Shared.Membership;

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