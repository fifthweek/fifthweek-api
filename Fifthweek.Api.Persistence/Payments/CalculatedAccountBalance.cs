namespace Fifthweek.Api.Persistence.Payments
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.CodeGeneration;

    /// <summary>
    /// This table is the sum of all the ledger entries plus uncommitted entries for a user.
    /// </summary>
    [AutoConstructor, AutoEqualityMembers, AutoSql]
    public partial class CalculatedAccountBalance
    {
        public CalculatedAccountBalance()
        {
        }

        [Required, Key, Column(Order = 0)]
        public Guid UserId { get; set; }

        [Required, Key, Column(Order = 1)]
        public LedgerAccountType AccountType { get; set; }

        [Required, Key, Column(Order = 2)]
        public DateTime Timestamp { get; set; }

        [Required]
        public decimal Amount { get; set; }

        public static string GetUserAccountBalanceQuery(string userIdParameterName, params CalculatedAccountBalance.Fields[] columns)
        {
            return GetUserAccountBalanceQuery(userIdParameterName, LedgerAccountType.FifthweekCredit, columns);
        }

        public static string GetUserAccountBalanceQuery(string userIdParameterName, LedgerAccountType accountType, params CalculatedAccountBalance.Fields[] columns)
        {
            var columnsString = string.Join(",", columns);

            return string.Format(
                @"SELECT TOP 1 {0} FROM {2} 
                        WHERE {3}=@{6} AND {4}={5}
                        ORDER BY {1} DESC",
            columnsString,
            CalculatedAccountBalance.Fields.Timestamp,
            CalculatedAccountBalance.Table,
            CalculatedAccountBalance.Fields.UserId,
            CalculatedAccountBalance.Fields.AccountType,
            (int)accountType,
            userIdParameterName);
        }

        public static string GetAccountBalancesQuery(LedgerAccountType accountType, params CalculatedAccountBalance.Fields[] columns)
        {
            var columnsString = string.Join(",", columns.Select(v => "t1." + v));

            // http://stackoverflow.com/a/123481
            return string.Format(
                @"SELECT {0} 
                    FROM {2} t1
                    LEFT OUTER JOIN {2} t2
                    ON t1.{3}=t2.{3} AND t1.{4}=t2.{4} AND t1.{1} < t2.{1}
                    WHERE t1.{4}={5} AND t2.{3} IS NULL",
            columnsString,
            CalculatedAccountBalance.Fields.Timestamp,
            CalculatedAccountBalance.Table,
            CalculatedAccountBalance.Fields.UserId,
            CalculatedAccountBalance.Fields.AccountType,
            (int)accountType);
        }
    }
}
