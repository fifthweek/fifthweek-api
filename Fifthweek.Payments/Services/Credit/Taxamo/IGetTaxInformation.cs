namespace Fifthweek.Payments.Services.Credit.Taxamo
{
    using System.Threading.Tasks;

    using Fifthweek.Shared;

    public interface IGetTaxInformation
    {
        //https://www.taxamo.com/apidocs/api/v1/tax/docs.html#POSTcalculate
        Task<TaxamoTransactionResult> ExecuteAsync(
            PositiveInt amount,
            string billingCountryCode,
            string creditCardPrefix,
            string ipAddress,
            string originalTaxamoTransactionKey);
    }
}