namespace Fifthweek.Payments.Services
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
    using Fifthweek.Shared;

    [AutoConstructor]
    public partial class UpdateAccountBalancesDbStatement : IUpdateAccountBalancesDbStatement
    {
        /// <summary>
        /// Note we calculate account balances pesimistically... we deduct uncommitted
        /// payments from subscribers accounts, but we don't add them to creators accounts.
        /// This means that users will generally be pleasantly surprised if the uncommitted
        /// estimates are wrong, rather than finding they had less money than expected.
        /// To include estimated income for creators, we could add this to the SQL:
        ///     UNION ALL
        ///     SELECT {6} AS userId, {9} AS accountType, {7} AS delta FROM {4}
        /// </summary>
        private static readonly string SqlStart = string.Format(
            @"INSERT INTO {0} 
                OUTPUT INSERTED.*
                SELECT u.userId, u.accountType, @Timestamp, SUM(u.delta)
                FROM (
                    SELECT {3} AS userId, {8} AS accountType, {1} AS delta FROM {2}
                    UNION ALL
                    SELECT {5} AS userId, {9} AS accountType, -1*{7} AS delta FROM {4}
                ) u
                ",
            CalculatedAccountBalance.Table,
            AppendOnlyLedgerRecord.Fields.Amount,
            AppendOnlyLedgerRecord.Table,
            AppendOnlyLedgerRecord.Fields.AccountOwnerId,
            UncommittedSubscriptionPayment.Table,
            UncommittedSubscriptionPayment.Fields.SubscriberId,
            UncommittedSubscriptionPayment.Fields.CreatorId,
            UncommittedSubscriptionPayment.Fields.Amount,
            AppendOnlyLedgerRecord.Fields.AccountType,
            (int)LedgerAccountType.FifthweekCredit);

        private static readonly string SqlUserIdFilter = string.Format(@"
                WHERE userId=@UserId");

        private static readonly string SqlEnd = string.Format(@"
                GROUP BY userId, accountType
                HAVING SUM(u.delta) != (
					SELECT COALESCE(
						(SELECT TOP 1 {0} FROM {1}
                        WHERE {2}=u.userId AND {3}=u.accountType
                        ORDER BY {4} DESC), 0))",
                CalculatedAccountBalance.Fields.Amount,
                CalculatedAccountBalance.Table,
                CalculatedAccountBalance.Fields.UserId,
                CalculatedAccountBalance.Fields.AccountType,
                CalculatedAccountBalance.Fields.Timestamp);

        private readonly IFifthweekDbConnectionFactory connectionFactory;

        public async Task<IReadOnlyList<CalculatedAccountBalanceResult>> ExecuteAsync(UserId userId, DateTime timestamp)
        {
            var sql = userId == null
                          ? string.Concat(SqlStart, SqlEnd)
                          : string.Concat(SqlStart, SqlUserIdFilter, SqlEnd);

            using (var connection = this.connectionFactory.CreateConnection())
            {
                var result = await connection.QueryAsync<CalculatedAccountBalance>(
                    sql,
                    new
                    {
                        UserId = userId == null ? Guid.Empty : userId.Value,
                        Timestamp = timestamp
                    });

                return result.Select(v => new CalculatedAccountBalanceResult(
                    DateTime.SpecifyKind(v.Timestamp, DateTimeKind.Utc),
                    new UserId(v.UserId),
                    v.AccountType,
                    v.Amount)).ToList();
            }
        }
    }
}