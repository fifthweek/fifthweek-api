namespace Fifthweek.Payments.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence.Payments;

    public interface IGetCalculatedAccountBalancesDbStatement
    {
        Task<IReadOnlyList<Snapshots.CalculatedAccountBalanceSnapshot>> ExecuteAsync(UserId userId, LedgerAccountType accountType, DateTime startTimestampInclusive, DateTime endTimestampExclusive);
    }
}