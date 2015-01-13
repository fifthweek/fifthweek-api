namespace Fifthweek.Api.Accounts
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Accounts.Controllers;
    using Fifthweek.Api.FileManagement;
    using Fifthweek.Api.Identity.Membership;

    public interface IAccountRepository
    {
        Task<GetAccountSettingsResult> GetAccountSettingsAsync(UserId userId);

        Task<AccountRepository.UpdateAccountSettingsResult> UpdateAccountSettingsAsync(
            UserId userId,
            ValidUsername newUsername,
            ValidEmail newEmail,
            ValidPassword newPassword,
            FileId newProfileImageFileId);
    }
}