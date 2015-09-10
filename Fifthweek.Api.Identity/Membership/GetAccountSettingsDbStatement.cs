namespace Fifthweek.Api.Identity.Membership
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Dapper;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Payments.Services;
    using Fifthweek.Shared;

    [AutoConstructor]
    public partial class GetAccountSettingsDbStatement : IGetAccountSettingsDbStatement
    {
        private static readonly string Sql = string.Format(
            @"SELECT u.{1}, u.{2}, u.{3}, cab.{4} as Balance, origin.{11},
                    CASE WHEN origin.{10} IS NULL OR origin.{13}={14} THEN 'False' ELSE 'True' END AS HasPaymentInformation,
                    cpo.{17}, cpo.{18}
                FROM {5} u
                LEFT OUTER JOIN ({8}) cab ON u.{6} = cab.{7}
                LEFT OUTER JOIN {9} origin ON u.{6} = origin.{12}
                LEFT OUTER JOIN {15} cpo ON u.{6}=cpo.{16}
                WHERE u.{6}=@UserId",
            FifthweekUser.Fields.Name,
            FifthweekUser.Fields.UserName,
            FifthweekUser.Fields.Email,
            FifthweekUser.Fields.ProfileImageFileId,
            CalculatedAccountBalance.Fields.Amount,
            FifthweekUser.Table,
            FifthweekUser.Fields.Id,
            CalculatedAccountBalance.Fields.UserId,
            CalculatedAccountBalance.GetUserAccountBalanceQuery("UserId", CalculatedAccountBalance.Fields.Amount, CalculatedAccountBalance.Fields.UserId),
            UserPaymentOrigin.Table,
            UserPaymentOrigin.Fields.PaymentOriginKey,
            UserPaymentOrigin.Fields.PaymentStatus,
            UserPaymentOrigin.Fields.UserId,
            UserPaymentOrigin.Fields.PaymentOriginKeyType,
            (int)PaymentOriginKeyType.None,
            CreatorPercentageOverride.Table,
            CreatorPercentageOverride.Fields.UserId,
            CreatorPercentageOverride.Fields.Percentage,
            CreatorPercentageOverride.Fields.ExpiryDate);

        private readonly IFifthweekDbConnectionFactory connectionFactory;

        public async Task<GetAccountSettingsDbResult> ExecuteAsync(UserId userId)
        {
            userId.AssertNotNull("userId");

            using (var connection = this.connectionFactory.CreateConnection())
            {
                var result = (await connection.QueryAsync<GetAccountSettingsDapperResult>(
                         Sql,
                         new { UserId = userId.Value })).SingleOrDefault();

                if (result == null)
                {
                    throw new DetailedRecoverableException(
                        "Unknown user.",
                        "The user ID " + userId.Value + " was not found in the database.");
                }

                var creatorPercentageOverrideData = result.Percentage == null
                    ? null
                    : new CreatorPercentageOverrideData(result.Percentage.Value, result.ExpiryDate);

                return new GetAccountSettingsDbResult(
                    new Username(result.UserName),
                    new Email(result.Email), 
                    result.ProfileImageFileId == null ? null : new FileId(result.ProfileImageFileId.Value),
                    result.Balance == null ? 0 : result.Balance.Value,
                    result.PaymentStatus == null ? PaymentStatus.None : result.PaymentStatus.Value,
                    result.HasPaymentInformation,
                    creatorPercentageOverrideData);
            }
        }

        private class GetAccountSettingsDapperResult
        {
            public string UserName { get; set; }

            public string Email { get; set; }

            public Guid? ProfileImageFileId { get; set; }

            public decimal? Balance { get; set; }

            public PaymentStatus? PaymentStatus { get; set; }

            public bool HasPaymentInformation { get; set; }
        
            public decimal? Percentage { get; set; }

            public DateTime? ExpiryDate { get; set; }
        }
    }

    [AutoConstructor, AutoEqualityMembers]
    public partial class GetAccountSettingsDbResult
    {
        public Username Username { get; private set; }

        public Email Email { get; private set; }

        [Optional]
        public FileId ProfileImageFileId { get; private set; }

        public decimal AccountBalance { get; private set; }

        public PaymentStatus PaymentStatus { get; private set; }

        public bool HasPaymentInformation { get; private set; }

        [Optional]
        public CreatorPercentageOverrideData CreatorPercentageOverride { get; private set; }
    }
}