namespace Fifthweek.Api.Payments.Commands
{
    using Fifthweek.Api.Payments.Taxamo;
    using Fifthweek.CodeGeneration;

    [AutoConstructor, AutoEqualityMembers]
    public partial class InitializeCreditRequestResult
    {
        public TaxamoTransactionResult TaxamoTransaction { get; private set; }

        public UserPaymentOriginResult Origin { get; private set; }
    }
}