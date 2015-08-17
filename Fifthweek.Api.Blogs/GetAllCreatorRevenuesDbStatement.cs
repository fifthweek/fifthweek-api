namespace Fifthweek.Api.Blogs
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
    public partial class GetAllCreatorRevenuesDbStatement : IGetAllCreatorRevenuesDbStatement
    {
        private static readonly string Sql = string.Format(
            @"SELECT unreleasedRevenue.{7}, 
                     unreleasedRevenue.{9} AS UnreleasedRevenue,
                     COALESCE(releasedRevenue.{9}, 0) AS ReleasedRevenue, 
                     COALESCE(retainedRevenue.RetainedRevenue, 0) AS RetainedRevenue,
                     u.{12} AS Username, u.{13}, u.{14}, COALESCE(u.{15}, 0) As EmailConfirmed
                FROM ({6})  unreleasedRevenue
                LEFT OUTER JOIN ({8}) releasedRevenue ON unreleasedRevenue.{7} = releasedRevenue.{7}
                LEFT OUTER JOIN (
                    SELECT {2}, SUM({0}) AS RetainedRevenue FROM {1}
                    WHERE {3}={4} AND ({5}>=@ReleasableDate AND {0}>0)
                    GROUP BY {2}) retainedRevenue
                    ON unreleasedRevenue.{7} = retainedRevenue.{2}
                LEFT OUTER JOIN {10} u ON unreleasedRevenue.{7}=u.{11}",
            AppendOnlyLedgerRecord.Fields.Amount,
            AppendOnlyLedgerRecord.Table,
            AppendOnlyLedgerRecord.Fields.AccountOwnerId,
            AppendOnlyLedgerRecord.Fields.AccountType,
            (int)LedgerAccountType.FifthweekRevenue,
            AppendOnlyLedgerRecord.Fields.Timestamp,
            CalculatedAccountBalance.GetAccountBalancesQuery(
                LedgerAccountType.FifthweekRevenue, 
                CalculatedAccountBalance.Fields.Amount,
                CalculatedAccountBalance.Fields.UserId),
            CalculatedAccountBalance.Fields.UserId,
            CalculatedAccountBalance.GetAccountBalancesQuery(
                LedgerAccountType.ReleasedRevenue, 
                CalculatedAccountBalance.Fields.Amount,
                CalculatedAccountBalance.Fields.UserId),
            CalculatedAccountBalance.Fields.Amount,
            FifthweekUser.Table,
            FifthweekUser.Fields.Id,
            FifthweekUser.Fields.UserName,
            FifthweekUser.Fields.Name,
            FifthweekUser.Fields.Email,
            FifthweekUser.Fields.EmailConfirmed);

        private readonly IFifthweekDbConnectionFactory connectionFactory;

        public async Task<GetAllCreatorRevenuesResult> ExecuteAsync(DateTime releasableRevenueDate)
        {
            using (var connection = this.connectionFactory.CreateConnection())
            {
                var result = await connection.QueryAsync<GetAllCreatorRevenueDbResult>(
                    Sql,
                    new
                    {
                        ReleasableDate = releasableRevenueDate
                    });

                return new GetAllCreatorRevenuesResult(result.Select(v => new GetAllCreatorRevenuesResult.Creator(
                    new UserId(v.UserId), 
                    (int)v.UnreleasedRevenue, 
                    (int)v.ReleasedRevenue, 
                    (int)(v.UnreleasedRevenue - v.RetainedRevenue),
                    v.Username == null ? null : new Username(v.Username),
                    v.Name == null ? null : new CreatorName(v.Name),
                    v.Email == null ? null : new Email(v.Email),
                    v.EmailConfirmed)).ToList());
            }
        }

        public class GetAllCreatorRevenueDbResult
        {
            public Guid UserId { get; set; }

            public decimal UnreleasedRevenue { get; set; }

            public decimal ReleasedRevenue { get; set; }

            public decimal RetainedRevenue { get; set; }

            public string Username { get; set; }
            
            public string Name { get; set; }

            public string Email { get; set; }

            public bool EmailConfirmed { get; set; }
        }
    }
}