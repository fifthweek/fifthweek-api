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
    public partial class GetCalculatedAccountBalancesDbStatement : IGetCalculatedAccountBalancesDbStatement
    {
        private static readonly string Sql = string.Format(
            @"SELECT * FROM {0}
              WHERE {1} = @UserId AND {3} = @AccountType
                  AND 
                  (
                      (
                          {2} >= @StartTimestampInclusive AND {2} < @EndTimestampExclusive
                      )
                      OR 
                      (
                          {2} = 
                          (
                              SELECT TOP 1 {2} 
                              FROM {0} 
                              WHERE {1}=@UserId AND {3} = @AccountType AND {2} < @StartTimestampInclusive ORDER BY {2} DESC
                          )
                      )
                  )",
            CalculatedAccountBalance.Table,
            CalculatedAccountBalance.Fields.UserId,
            CalculatedAccountBalance.Fields.Timestamp,
            CalculatedAccountBalance.Fields.AccountType);

        private readonly IFifthweekDbConnectionFactory connectionFactory;

        public async Task<IReadOnlyList<Snapshots.CalculatedAccountBalanceSnapshot>> ExecuteAsync(UserId userId, LedgerAccountType accountType, DateTime startTimestampInclusive, DateTime endTimestampExclusive)
        {
            using (PaymentsPerformanceLogger.Instance.Log(typeof(GetCalculatedAccountBalancesDbStatement)))
            using (var connection = this.connectionFactory.CreateConnection())
            {
                var databaseResult = await connection.QueryAsync<CalculatedAccountBalance>(
                    Sql,
                    new
                    {
                        UserId = userId == null ? Guid.Empty : userId.Value,
                        AccountType = accountType,
                        StartTimestampInclusive = startTimestampInclusive,
                        EndTimestampExclusive = endTimestampExclusive
                    });

                return databaseResult
                    .Select(v => new Snapshots.CalculatedAccountBalanceSnapshot(
                        DateTime.SpecifyKind(v.Timestamp, DateTimeKind.Utc),
                        new UserId(v.UserId),
                        v.AccountType,
                        v.Amount))
                    .ToList();
            }
        }
    }
}