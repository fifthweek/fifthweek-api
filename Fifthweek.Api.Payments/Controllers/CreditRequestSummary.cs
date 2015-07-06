namespace Fifthweek.Api.Payments.Controllers
{
    using Fifthweek.CodeGeneration;

    [AutoConstructor, AutoEqualityMembers]
    public partial class CreditRequestSummary
    {
        public int Amount { get; private set; }

        public int TotalAmount { get; private set; }

        public int TaxAmount { get; private set; }

        public decimal TaxRate { get; private set; }

        public string TaxName { get; private set; }

        public string TaxEntityName { get; private set; }

        public string CountryName { get; private set; }
    }
}