namespace Fifthweek.Payments.Taxamo
{
    using System.Threading.Tasks;

    using global::Taxamo.Api;
    using global::Taxamo.Client;
    using global::Taxamo.Model;

    public class TaxamoService : ITaxamoService
    {
        public Task<CalculateTaxOut> CalculateTaxAsync(CalculateTaxIn input, string apiKey)
        {           
            var tax = new ApivtaxApi(new ApiClient(apiKey));
            return tax.CalculateTaxAsync(input);
        }
    }
}