namespace Fifthweek.Payments.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence.Payments;

    public interface IUpdateAccountBalancesDbStatement
    {
        Task<IReadOnlyList<CalculatedAccountBalanceResult>> ExecuteAsync(UserId userId, DateTime timestamp);
    }
}