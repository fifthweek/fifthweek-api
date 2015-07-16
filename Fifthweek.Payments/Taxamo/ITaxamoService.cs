namespace Fifthweek.Payments.Taxamo
{
    using System.Threading.Tasks;

    using global::Taxamo.Model;

    public interface ITaxamoService
    {
        Task<CalculateTaxOut> CalculateTaxAsync(CalculateTaxIn input, string apiKey);

        Task<CreateTransactionOut> CreateTransactionAsync(CreateTransactionIn input, string apiKey);

        Task<CreatePaymentOut> CreatePaymentAsync(string transactionKey, CreatePaymentIn input, string apiKey);

        Task<CancelTransactionOut> CancelTransactionAsync(string transactionKey, string apiKey);
    }
}