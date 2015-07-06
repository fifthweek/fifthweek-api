namespace Fifthweek.Api.Payments.Taxamo
{
    using Fifthweek.CodeGeneration;

    [AutoConstructor, AutoEqualityMembers]
    public partial class TaxamoTransactionResult
    {
        [Optional]
        public string Key { get; private set; }

        public AmountInUsCents Amount { get; private set; }

        public AmountInUsCents TotalAmount { get; private set; }

        public AmountInUsCents TaxAmount { get; private set; }

        public decimal TaxRate { get; private set; }

        public string TaxName { get; private set; }

        public string TaxEntityName { get; private set; }

        public string CountryName { get; private set; }
    }
}