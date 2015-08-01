namespace Fifthweek.Payments.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Dapper;

    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;

    [AutoConstructor]
    public partial class PersistCommittedAndUncommittedRecordsDbStatement : IPersistCommittedAndUncommittedRecordsDbStatement
    {
        private readonly IFifthweekDbConnectionFactory connectionFactory;

        public async Task ExecuteAsync(
            UserId subscriberId,
            UserId creatorId,
            IReadOnlyList<AppendOnlyLedgerRecord> ledgerRecords, 
            UncommittedSubscriptionPayment uncommittedRecord)
        {
            subscriberId.AssertNotNull("subscriberId");
            creatorId.AssertNotNull("creatorId");
            ledgerRecords.AssertNotNull("ledgerRecords");

            using (PaymentsPerformanceLogger.Instance.Log(typeof(PersistCommittedAndUncommittedRecordsDbStatement)))
            using (var transaction = TransactionScopeBuilder.CreateAsync())
            {
                using (var connection = this.connectionFactory.CreateConnection())
                {
                    if (ledgerRecords.Any())
                    {
                        await connection.InsertAsync(ledgerRecords, false);
                    }

                    if (uncommittedRecord != null)
                    {
                        var uncommittedFields = UncommittedSubscriptionPayment.Fields.StartTimestampInclusive
                                                | UncommittedSubscriptionPayment.Fields.EndTimestampExclusive
                                                | UncommittedSubscriptionPayment.Fields.Amount
                                                | UncommittedSubscriptionPayment.Fields.InputDataReference;

                        await connection.UpsertAsync(uncommittedRecord, uncommittedFields);
                    }
                    else
                    {
                        // Remove the existing uncommitted record.
                        await connection.ExecuteAsync(
                            string.Format(
                                @"DELETE FROM {0} WHERE {1}=@SubscriberId AND {2}=@CreatorId",
                                UncommittedSubscriptionPayment.Table,
                                UncommittedSubscriptionPayment.Fields.SubscriberId,
                                UncommittedSubscriptionPayment.Fields.CreatorId),
                            new 
                            {
                                SubscriberId = subscriberId.Value,
                                CreatorId = creatorId.Value
                            });
                    }
                }

                transaction.Complete();
            }
        }
    }
}