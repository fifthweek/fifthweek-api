namespace Fifthweek.Payments.Services.Credit.Taxamo
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Shared;

    public interface ICommitTaxamoTransaction
    {
        Task ExecuteAsync(
            TaxamoTransactionResult taxamoTransactionResult,
            StripeTransactionResult stripeTransactionResult,
            UserType userType);
    }
}