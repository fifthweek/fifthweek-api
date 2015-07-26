namespace Fifthweek.Payments.Services.Credit
{
    using System.Linq;
    using System.Threading.Tasks;

    using Dapper;

    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;

    [AutoConstructor]
    public partial class GetUserPaymentOriginDbStatement : IGetUserPaymentOriginDbStatement
    {
        private static readonly string Sql = string.Format(
            "SELECT * FROM {0} WHERE {1}=@UserId",
            UserPaymentOrigin.Table,
            UserPaymentOrigin.Fields.UserId);

        private readonly IFifthweekDbConnectionFactory connectionFactory;

        public async Task<UserPaymentOriginResult> ExecuteAsync(UserId userId)
        {
            userId.AssertNotNull("userId");

            using (var connection = this.connectionFactory.CreateConnection())
            {
                var results = await connection.QueryAsync<UserPaymentOrigin>(
                    Sql,
                    new
                    {
                        UserId = userId.Value
                    });

                var result = results.FirstOrDefault();

                if (result == null)
                {
                    return UserPaymentOriginResult.Empty;
                }

                return new UserPaymentOriginResult(
                    result.PaymentOriginKey,
                    result.PaymentOriginKeyType,
                    result.CountryCode,
                    result.CreditCardPrefix,
                    result.IpAddress,
                    result.OriginalTaxamoTransactionKey,
                    result.PaymentStatus);
            }
        }
    }
}