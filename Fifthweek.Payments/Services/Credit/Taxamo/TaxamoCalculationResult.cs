namespace Fifthweek.Payments.Services.Credit.Taxamo
{
    using System.Collections.Generic;

    using Fifthweek.CodeGeneration;

    [AutoConstructor, AutoEqualityMembers]
    public partial class TaxamoCalculationResult
    {
        public AmountInMinorDenomination Amount { get; private set; }

        public AmountInMinorDenomination TotalAmount { get; private set; }

        public AmountInMinorDenomination TaxAmount { get; private set; }

        [Optional]
        public decimal? TaxRate { get; private set; }

        [Optional]
        public string TaxName { get; private set; }

        [Optional]
        public string TaxEntityName { get; private set; }

        [Optional]
        public string CountryName { get; private set; }

        [Optional]
        public IReadOnlyList<PossibleCountry> PossibleCountries { get; private set; }

        [AutoConstructor, AutoEqualityMembers]
        public partial class PossibleCountry
        {
            public string Name { get; private set; }

            public string CountryCode { get; private set; }
        }
    }
}