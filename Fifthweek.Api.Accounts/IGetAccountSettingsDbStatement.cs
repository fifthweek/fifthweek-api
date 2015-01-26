namespace Fifthweek.Api.Accounts
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Identity.Membership;

    public interface IGetAccountSettingsDbStatement
    {
        Task<GetAccountSettingsResult> ExecuteAsync(UserId userId);
    }
}