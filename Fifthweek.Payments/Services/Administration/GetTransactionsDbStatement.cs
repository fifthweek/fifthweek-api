namespace Fifthweek.Payments.Services.Administration
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Dapper;

    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Payments.Shared;
    using Fifthweek.Shared;

    [AutoConstructor]
    public partial class GetTransactionsDbStatement : IGetTransactionsDbStatement
    {
        // Return credit, subscription payments and refunds.
        private static readonly string GetTransactionRowsSql = string.Format(
           @"SELECT l.*, u1.{6} AS AccountOwnerUsername, u2.{6} AS CounterpartyUsername
            FROM {0} l
                LEFT JOIN {3} u1 ON l.{1}=u1.{4}
                LEFT JOIN {3} u2 ON l.{5}=u2.{4}
            WHERE 
                l.{7} IN (SELECT {7} FROM {0} WHERE {2}>=@StartTime AND {2}<@EndTime AND (@UserId IS NULL OR {1}=@UserId))
            ORDER BY l.{2} DESC",
           AppendOnlyLedgerRecord.Table,
           AppendOnlyLedgerRecord.Fields.AccountOwnerId,
           AppendOnlyLedgerRecord.Fields.Timestamp,
           FifthweekUser.Table,
           FifthweekUser.Fields.Id,
           AppendOnlyLedgerRecord.Fields.CounterpartyId,
           FifthweekUser.Fields.UserName,
           AppendOnlyLedgerRecord.Fields.TransactionReference);

        private readonly IFifthweekDbConnectionFactory connectionFactory;

        public async Task<GetTransactionsResult> ExecuteAsync(UserId userId, DateTime startTimeInclusive, DateTime endTimeExclusive)
        {
            using (var connection = this.connectionFactory.CreateConnection())
            {
                var records = (await connection.QueryAsync<DatabaseResult>(
                    GetTransactionRowsSql,
                    new 
                    {
                        UserId = userId == null ? (Guid?)null : userId.Value,
                        StartTime = startTimeInclusive,
                        EndTime = endTimeExclusive,
                    })).ToList();

                return new GetTransactionsResult(
                    records.Select(
                    v => new GetTransactionsResult.Item(
                        v.Id,
                        new UserId(v.AccountOwnerId),
                        v.AccountOwnerUsername == null ? null : new Username(v.AccountOwnerUsername),
                        v.CounterpartyId == null ? null : new UserId(v.CounterpartyId.Value),
                        v.CounterpartyUsername == null ? null : new Username(v.CounterpartyUsername),
                        DateTime.SpecifyKind(v.Timestamp, DateTimeKind.Utc),
                        v.Amount,
                        v.AccountType,
                        v.TransactionType,
                        new TransactionReference(v.TransactionReference),
                        v.InputDataReference,
                        v.Comment,
                        v.StripeChargeId,
                        v.TaxamoTransactionKey)).ToList());
            }
        }

        private class DatabaseResult : AppendOnlyLedgerRecord
        {
            public DatabaseResult()
            {
            }

            public string AccountOwnerUsername { get; set; }

            public string CounterpartyUsername { get; set; }
        }
    }
}