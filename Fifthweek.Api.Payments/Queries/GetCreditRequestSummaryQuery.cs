namespace Fifthweek.Api.Payments.Queries
{
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Payments.Controllers;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Payments.Services.Credit.Taxamo;
    using Fifthweek.Shared;

    [AutoConstructor, AutoEqualityMembers]
    public partial class GetCreditRequestSummaryQuery : IQuery<CreditRequestSummary>
    {
        public Requester Requester { get; private set; }

        public UserId UserId { get; private set; }

        [Optional]
        public LocationData LocationDataOverride { get; private set; }

        [AutoConstructor, AutoEqualityMembers]
        public partial class LocationData
        {
            [Optional]
            public ValidCountryCode CountryCode { get; private set; }

            [Optional]
            public ValidCreditCardPrefix CreditCardPrefix { get; private set; }

            [Optional]
            public ValidIpAddress IpAddress { get; private set; }
        }
    }
}