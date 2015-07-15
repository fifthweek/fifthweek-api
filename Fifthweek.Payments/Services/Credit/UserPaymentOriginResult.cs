﻿namespace Fifthweek.Payments.Services.Credit
{
    using Fifthweek.Api.Persistence.Payments;
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
        public string CountryCode { get; private set; }

        [Optional]
        public string CreditCardPrefix { get; private set; }

        [Optional]
        public string IpAddress { get; private set; }

        [Optional]
        public string OriginalTaxamoTransactionKey { get; private set; }

        [Optional]
        public PaymentStatus PaymentStatus { get; private set; }
    }
}