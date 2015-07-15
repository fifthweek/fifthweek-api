namespace Fifthweek.Payments.Taxamo
{
    using System.Threading.Tasks;

    using global::Taxamo.Model;

    public interface ITaxamoService
    {
        Task<CalculateTaxOut> CalculateTaxAsync(CalculateTaxIn input, string apiKey);
    }
}