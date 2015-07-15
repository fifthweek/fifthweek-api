namespace Fifthweek.Api.Payments.Controllers
{
    using Fifthweek.CodeGeneration;
    using Fifthweek.Payments.Services.Credit.Taxamo;

    [AutoConstructor, AutoEqualityMembers]
    public partial class CreditRequestSummary
    {
        public int SubscriptionsAmount { get; private set; }

        public TaxamoCalculationResult Calculation { get; private set; }
    }
}