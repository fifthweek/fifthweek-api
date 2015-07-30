namespace Fifthweek.Payments.Services.Administration
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Fifthweek.Api.Identity.Shared.Membership;

    public interface IGetTransactionsDbStatement
    {
        Task<GetTransactionsResult> ExecuteAsync(UserId userId, DateTime startTimeInclusive, DateTime endTimeExclusive);
    }
}