namespace Fifthweek.Payments.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence.Payments;

    public interface IPersistCommittedAndUncommittedRecordsDbStatement
    {
        Task ExecuteAsync(UserId subscriberId, UserId creatorId, IReadOnlyList<AppendOnlyLedgerRecord> ledgerRecords, UncommittedSubscriptionPayment uncommittedRecord);
    }
}