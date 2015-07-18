namespace Fifthweek.Payments.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Dapper;

    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.Api.Persistence.Snapshots;
    using Fifthweek.CodeGeneration;

    [AutoConstructor]
    public partial class GetLatestCommittedLedgerDateDbStatement : IGetLatestCommittedLedgerDateDbStatement
    {
        private static readonly string Sql = string.Format(
            @"SELECT TOP 1 {0} FROM {1}
                WHERE {2}=@SubscriberId AND {3}=@CreatorId AND {4}={5}
                ORDER BY {0} DESC",
            AppendOnlyLedgerRecord.Fields.Timestamp,
            AppendOnlyLedgerRecord.Table,
            AppendOnlyLedgerRecord.Fields.AccountOwnerId,
            AppendOnlyLedgerRecord.Fields.CounterpartyId,
            AppendOnlyLedgerRecord.Fields.TransactionType,
            (int)LedgerTransactionType.SubscriptionPayment);

        private readonly IFifthweekDbConnectionFactory connectionFactory;

        public async Task<DateTime?> ExecuteAsync(UserId subscriberId, UserId creatorId)
        {
            using (var connection = this.connectionFactory.CreateConnection())
            {
                var result = await connection.ExecuteScalarAsync<DateTime?>(
                    Sql,
                    new 
                    {
                        SubscriberId = subscriberId.Value,
                        CreatorId = creatorId.Value
                    });

                if (result == null)
                {
                    return null;
                }
             
                var utcResult = DateTime.SpecifyKind(result.Value, DateTimeKind.Utc);
                return utcResult;
            }
        }
    }
}