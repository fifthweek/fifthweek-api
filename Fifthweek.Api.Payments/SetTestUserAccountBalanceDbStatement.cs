namespace Fifthweek.Api.Payments
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
    public partial class SetTestUserAccountBalanceDbStatement : ISetTestUserAccountBalanceDbStatement
    {
        private static readonly string Sql = string.Format(
            @"INSERT INTO {0} ({1}, {2}, {3}, {4})
                VALUES (@UserId, @Timestamp, @Amount, {5})",
            CalculatedAccountBalance.Table,
            CalculatedAccountBalance.Fields.UserId,
            CalculatedAccountBalance.Fields.Timestamp,
            CalculatedAccountBalance.Fields.Amount,
            CalculatedAccountBalance.Fields.AccountType,
            (int)LedgerAccountType.Fifthweek);

        private readonly IFifthweekDbConnectionFactory connectionFactory;

        public async Task ExecuteAsync(UserId userId, DateTime timestamp, PositiveInt amount)
        {
            userId.AssertNotNull("userId");
            amount.AssertNotNull("amount");

            using (var connection = this.connectionFactory.CreateConnection())
            {
                await connection.ExecuteAsync(
                    Sql,
                    new
                    {
                        UserId = userId.Value,
                        Timestamp = timestamp,
                        Amount = amount.Value,
                    });
            }
        }
    }
}