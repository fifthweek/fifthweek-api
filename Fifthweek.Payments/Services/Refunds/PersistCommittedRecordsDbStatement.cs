namespace Fifthweek.Payments.Services.Refunds
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.CodeGeneration;

    [AutoConstructor]
    public partial class PersistCommittedRecordsDbStatement : IPersistCommittedRecordsDbStatement
    {
        private readonly IFifthweekDbConnectionFactory connectionFactory;

        public async Task ExecuteAsync(IReadOnlyList<AppendOnlyLedgerRecord> records)
        {
            using (var connection = this.connectionFactory.CreateConnection())
            {
                await connection.InsertAsync(records);
            }
        }
    }
}