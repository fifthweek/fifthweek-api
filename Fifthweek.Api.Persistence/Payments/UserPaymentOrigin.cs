namespace Fifthweek.Api.Persistence.Payments
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.CodeGeneration;

    [AutoConstructor, AutoEqualityMembers, AutoSql]
    public partial class UserPaymentOrigin
    {
        public UserPaymentOrigin()
        {
        }

        [Required, Key, ForeignKey("User")]
        public Guid UserId { get; set; }

        [Required, Optional, NonEquatable]
        public FifthweekUser User { get; set; }

        [Optional, MaxLength(255)] // See: https://stripe.com/docs/upgrades#what-changes-does-stripe-consider-to-be-backwards-compatible
        public string StripeCustomerId { get; set; }

        [Optional, MaxLength(3)] // See: https://en.wikipedia.org/wiki/ISO_3166
        public string BillingCountryCode { get; set; }

        [Optional, MaxLength(6)] // See: https://www.taxamo.com/doc/taxamojs/storing_transactions/
        public string CreditCardPrefix { get; set; }

        [Optional, MaxLength(45)] // See: http://stackoverflow.com/questions/1076714/max-length-for-client-ip-address
        public string IpAddress { get; set; }

        [Optional, MaxLength(255)] // Arbitrarily taken as same length of stripe IDs.
        public string OriginalTaxamoTransactionKey { get; set; }
    }
}