namespace Fifthweek.Api.Accounts
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Identity.Membership;

    using UserId = Fifthweek.Api.Identity.Shared.Membership.UserId;

    public interface IGetAccountSettingsDbStatement
    {
        Task<GetAccountSettingsResult> ExecuteAsync(UserId userId);
    }
}