namespace Fifthweek.Payments.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;

    [AutoConstructor]
    public partial class PersistCommittedAndUncommittedRecordsDbStatement : IPersistCommittedAndUncommittedRecordsDbStatement
    {
        private readonly IFifthweekDbConnectionFactory connectionFactory;

        public async Task ExecuteAsync(IReadOnlyList<AppendOnlyLedgerRecord> ledgerRecords, UncommittedSubscriptionPayment uncommittedRecord)
        {
            ledgerRecords.AssertNotNull("ledgerRecords");

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
                }

                transaction.Complete();
            }
        }
    }
}