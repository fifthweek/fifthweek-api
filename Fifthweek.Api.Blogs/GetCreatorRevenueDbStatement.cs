namespace Fifthweek.Api.Blogs
{
    using System;
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
        private static readonly string Sql = CalculatedAccountBalance.GetUserAccountBalanceQuery(
            "UserId", LedgerAccountType.FifthweekRevenue, CalculatedAccountBalance.Fields.Amount);

        private readonly IFifthweekDbConnectionFactory connectionFactory;

        public async Task<GetCreatorRevenueDbStatementResult> ExecuteAsync(UserId userId)
        {
            userId.AssertNotNull("userId");

            using (var connection = this.connectionFactory.CreateConnection())
            {
                var amount = await connection.ExecuteScalarAsync<int>(
                    Sql,
                    new
                    {
                        UserId = userId.Value,
                    });

                return new GetCreatorRevenueDbStatementResult(amount);
            }
        }

        [AutoConstructor, AutoEqualityMembers]
        public partial class GetCreatorRevenueDbStatementResult
        {
            public int TotalRevenue { get; private set; }
        }
    }
}