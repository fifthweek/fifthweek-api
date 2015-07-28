namespace Fifthweek.Payments.Services.Refunds
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.Payments.Shared;

    public interface IGetRecordsForTransactionDbStatement
    {
        Task<IReadOnlyList<AppendOnlyLedgerRecord>> ExecuteAsync(TransactionReference transactionReference);
    }
}