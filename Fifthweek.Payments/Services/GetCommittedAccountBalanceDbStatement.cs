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
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;

    [AutoConstructor]
    public partial class GetCommittedAccountBalanceDbStatement : IGetCommittedAccountBalanceDbStatement
    {
        private static readonly string Sql = string.Format(
            @"SELECT SUM({0})
                FROM {1}
                WHERE {2}=@UserId AND {3}={4}",
            AppendOnlyLedgerRecord.Fields.Amount,
            AppendOnlyLedgerRecord.Table,
            AppendOnlyLedgerRecord.Fields.AccountOwnerId,
            AppendOnlyLedgerRecord.Fields.AccountType,
            (int)LedgerAccountType.FifthweekCredit);

        private readonly IFifthweekDbConnectionFactory connectionFactory;

        public async Task<decimal> ExecuteAsync(UserId userId)
        {
            userId.AssertNotNull("userId");

            using (PaymentsPerformanceLogger.Instance.Log(typeof(GetCommittedAccountBalanceDbStatement)))
            using (var connection = this.connectionFactory.CreateConnection())
            {
                var result = await connection.ExecuteScalarAsync<decimal>(
                    Sql,
                    new
                    {
                        UserId = userId.Value
                    });

                return result;
            }
        }
    }
}