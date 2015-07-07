namespace Fifthweek.Payments.Services.Credit.Taxamo
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Shared;

    public class CreateTaxamoTransaction : ICreateTaxamoTransaction
    {
        public async Task<TaxamoTransactionResult> ExecuteAsync(
            PositiveInt amount,
            string billingCountryCode,
            string creditCardPrefix,
            string ipAddress,
            string originalTaxamoTransactionKey)
        {
            throw new NotImplementedException();
        }
    }
}