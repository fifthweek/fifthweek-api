namespace Fifthweek.Api.Payments.Taxamo
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Payments.Controllers;
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