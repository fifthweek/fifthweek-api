namespace Fifthweek.Api.Payments
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;

    [AutoConstructor]
    public partial class SetUserPaymentOriginDbStatement : ISetUserPaymentOriginDbStatement
    {
        private readonly IFifthweekDbConnectionFactory connectionFactory;

        public async Task ExecuteAsync(
            UserId userId,
            string stripeCustomerId,
            ValidCountryCode billingCountryCode,
            ValidCreditCardPrefix creditCardPrefix,
            ValidIpAddress ipAddress)
        {
            userId.AssertNotNull("userId");

            // The credit card has changed so the original transaction key can no longer be used for tax purposes
            // as it is based on the original card prefix.
            var origin = new UserPaymentOrigin(
                userId.Value,
                null,
                stripeCustomerId,
                billingCountryCode == null ? null : billingCountryCode.Value,
                creditCardPrefix == null ? null : creditCardPrefix.Value,
                ipAddress == null ? null : ipAddress.Value,
                null,
                default(PaymentStatus));

            using (var connection = this.connectionFactory.CreateConnection())
            {
                await connection.UpsertAsync(origin, UserPaymentOrigin.Fields.StripeCustomerId | UserPaymentOrigin.Fields.CountryCode | UserPaymentOrigin.Fields.CreditCardPrefix | UserPaymentOrigin.Fields.IpAddress | UserPaymentOrigin.Fields.OriginalTaxamoTransactionKey);
            }
        }
    }
}