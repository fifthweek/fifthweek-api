namespace Fifthweek.Payments.Services.Credit.Taxamo
{
    using System.Threading.Tasks;

    using Fifthweek.Shared;

    public interface ICreateTaxamoTransaction
    {
        Task<TaxamoTransactionResult> ExecuteAsync(
            PositiveInt amount,
            string billingCountryCode,
            string creditCardPrefix,
            string ipAddress,
            string originalTaxamoTransactionKey,
            UserType userType);
    }
}