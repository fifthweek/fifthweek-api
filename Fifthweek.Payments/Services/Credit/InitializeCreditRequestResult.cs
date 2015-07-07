namespace Fifthweek.Payments.Services.Credit
{
    using Fifthweek.CodeGeneration;
    using Fifthweek.Payments.Services.Credit.Taxamo;

    [AutoConstructor, AutoEqualityMembers]
    public partial class InitializeCreditRequestResult
    {
        public TaxamoTransactionResult TaxamoTransaction { get; private set; }

        public UserPaymentOriginResult Origin { get; private set; }
    }
}