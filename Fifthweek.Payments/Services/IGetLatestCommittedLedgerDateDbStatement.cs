namespace Fifthweek.Payments.Services
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Identity.Shared.Membership;

    public interface IGetLatestCommittedLedgerDateDbStatement
    {
        Task<DateTime?> ExecuteAsync(UserId subscriberId, UserId creatorId);
    }
}