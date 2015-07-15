namespace Fifthweek.Api.Payments.Controllers
{
    using Fifthweek.CodeGeneration;

    [AutoConstructor, AutoEqualityMembers]
    public partial class PaymentLocationData
    {
        public PaymentLocationData()
        {
        }

        [Optional, Parsed(typeof(ValidCountryCode))]
        public string CountryCode { get; set; }

        [Optional, Parsed(typeof(ValidCreditCardPrefix))]
        public string CreditCardPrefix { get; set; }

        [Optional, Parsed(typeof(ValidIpAddress))]
        public string IpAddress { get; set; }
    }
}