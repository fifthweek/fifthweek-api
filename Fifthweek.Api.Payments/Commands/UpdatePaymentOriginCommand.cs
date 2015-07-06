namespace Fifthweek.Api.Payments.Commands
{
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;

    [AutoConstructor, AutoEqualityMembers]
    public partial class UpdatePaymentOriginCommand
    {
        public Requester Requester { get; private set; }

        public UserId UserId { get; private set; }

        [Optional]
        public ValidStripeToken StripeToken { get; private set; }

        [Optional]
        public ValidCountryCode BillingCountryCode { get; private set; }

        [Optional]
        public ValidCreditCardPrefix CreditCardPrefix { get; private set; }

        [Optional]
        public ValidIpAddress IpAddress { get; private set; }
    }
}