namespace Fifthweek.Api.Payments
{
    using Fifthweek.CodeGeneration;

    [AutoConstructor, AutoEqualityMembers]
    public partial class UserPaymentOriginResult
    {
        public static readonly UserPaymentOriginResult Empty = new UserPaymentOriginResult();

        protected UserPaymentOriginResult()
        {
        }

        [Optional]
        public string StripeCustomerId { get; private set; }

        [Optional]
        public string BillingCountryCode { get; private set; }

        [Optional]
        public string CreditCardPrefix { get; private set; }

        [Optional]
        public string IpAddress { get; private set; }

        [Optional]
        public string OriginalTaxamoTransactionKey { get; private set; }
    }
}