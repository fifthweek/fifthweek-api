namespace Fifthweek.Payments.Services.Refunds
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Fifthweek.Api.Persistence.Payments;

    public interface IPersistCommittedRecordsDbStatement
    {
        Task ExecuteAsync(IReadOnlyList<AppendOnlyLedgerRecord> records);
    }
}