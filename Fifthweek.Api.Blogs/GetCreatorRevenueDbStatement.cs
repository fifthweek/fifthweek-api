namespace Fifthweek.Api.Blogs
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Dapper;

    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;

    [AutoConstructor]
    public partial class GetCreatorRevenueDbStatement : IGetCreatorRevenueDbStatement
    {
        private static readonly string UnreleasedRevenueSql = CalculatedAccountBalance.GetUserAccountBalanceQuery(
            "UserId", LedgerAccountType.FifthweekRevenue, CalculatedAccountBalance.Fields.Amount);

        private static readonly string ReleasedRevenueSql = CalculatedAccountBalance.GetUserAccountBalanceQuery(
            "UserId", LedgerAccountType.ReleasedRevenue, CalculatedAccountBalance.Fields.Amount);

        private static readonly string RetainedRevenueSql = string.Format(
            @"SELECT SUM({0}) FROM {1} WHERE {2}=@UserId AND {3}={4} AND ({5}>=@ReleasableDate AND {0}>0);",
            AppendOnlyLedgerRecord.Fields.Amount,
            AppendOnlyLedgerRecord.Table,
            AppendOnlyLedgerRecord.Fields.AccountOwnerId,
            AppendOnlyLedgerRecord.Fields.AccountType,
            (int)LedgerAccountType.FifthweekRevenue,
            AppendOnlyLedgerRecord.Fields.Timestamp);

        private static readonly string Sql = string.Concat(
            UnreleasedRevenueSql,
            ";",
            ReleasedRevenueSql,
            ";",
            RetainedRevenueSql);

        private readonly IFifthweekDbConnectionFactory connectionFactory;

        public async Task<GetCreatorRevenueDbStatementResult> ExecuteAsync(UserId userId, DateTime releasableRevenueDate)
        {
            userId.AssertNotNull("userId");

            using (var connection = this.connectionFactory.CreateConnection())
            {
                using (var multi = await connection.QueryMultipleAsync(
                    Sql,
                    new
                    {
                        UserId = userId.Value,
                        ReleasableDate = releasableRevenueDate
                    }))
                {
                    var unreleasedRevenue = (await multi.ReadAsync<decimal?>()).FirstOrDefault() ?? 0;
                    var releasedRevenue = (await multi.ReadAsync<decimal?>()).FirstOrDefault() ?? 0;
                    var retainedRevenue = (await multi.ReadAsync<decimal?>()).FirstOrDefault() ?? 0;

                    return new GetCreatorRevenueDbStatementResult((int)unreleasedRevenue, (int)releasedRevenue, (int)(unreleasedRevenue - retainedRevenue));
                }
            }
        }

        [AutoConstructor, AutoEqualityMembers]
        public partial class GetCreatorRevenueDbStatementResult
        {
            public int UnreleasedRevenue { get; private set; }

            public int ReleasedRevenue { get; private set; }

            public int ReleasableRevenue { get; private set; }
        }
    }
}