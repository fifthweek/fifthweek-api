namespace Fifthweek.Payments.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Fifthweek.Api.Persistence.Payments;

    public interface IPersistCommittedAndUncommittedRecordsDbStatement
    {
        Task ExecuteAsync(IReadOnlyList<AppendOnlyLedgerRecord> ledgerRecords, UncommittedSubscriptionPayment uncommittedRecord);
    }
}