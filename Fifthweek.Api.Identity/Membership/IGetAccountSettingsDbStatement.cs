namespace Fifthweek.Api.Identity.Membership
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Identity.Shared.Membership;

    public interface IGetAccountSettingsDbStatement
    {
        Task<GetAccountSettingsDbResult> ExecuteAsync(UserId userId, DateTime freePostTimestamp, int maximumFreePosts);
    }
}