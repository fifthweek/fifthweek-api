namespace Fifthweek.Payments.Services.Refunds
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Dapper;

    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Payments.Shared;
    using Fifthweek.Shared;

    [AutoConstructor]
    public partial class GetRecordsForTransactionDbStatement : IGetRecordsForTransactionDbStatement
    {
        private static readonly string GetTransactionRowsSql = string.Format(
            @"SELECT * FROM {0} WHERE {1}=@TransactionReference",
            AppendOnlyLedgerRecord.Table,
            AppendOnlyLedgerRecord.Fields.TransactionReference);

        private readonly IFifthweekDbConnectionFactory connectionFactory;

        public async Task<IReadOnlyList<AppendOnlyLedgerRecord>> ExecuteAsync(TransactionReference transactionReference)
        {
            transactionReference.AssertNotNull("transactionReference");

            using (var connection = this.connectionFactory.CreateConnection())
            {
                var records = (await connection.QueryAsync<AppendOnlyLedgerRecord>(
                    GetTransactionRowsSql,
                    new { TransactionReference = transactionReference.Value })).ToList();

                return records;
            }
        }
    }
}