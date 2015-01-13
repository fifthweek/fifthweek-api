namespace Fifthweek.Api.Accounts
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Accounts.Controllers;
    using Fifthweek.Api.FileManagement;
    using Fifthweek.Api.Identity.Membership;

    public interface IAccountRepository
    {
        Task<AccountSettingsResult> GetAccountSettingsAsync(UserId userId);

        Task<AccountRepository.UpdateAccountSettingsResult> UpdateAccountSettingsAsync(
            UserId userId,
            Username newUsername,
            Email newEmail,
            Password newPassword,
            FileId newProfileImageFileId);
    }
}