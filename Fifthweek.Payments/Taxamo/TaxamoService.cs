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

        public Task<CreateTransactionOut> CreateTransactionAsync(CreateTransactionIn input, string apiKey)
        {
            var transaction = new ApivtransactionsApi(new ApiClient(apiKey));
            return transaction.CreateTransactionAsync(input);
        }

        public Task<CreatePaymentOut> CreatePaymentAsync(string transactionKey, CreatePaymentIn input, string apiKey)
        {
            var transactionPayments = new ApivtransactionskeypaymentsApi(new ApiClient(apiKey));
            return transactionPayments.CreatePaymentAsync(transactionKey, input);
        }

        public Task<CancelTransactionOut> CancelTransactionAsync(string transactionKey, string apiKey)
        {
            var transaction = new ApivtransactionsApi(new ApiClient(apiKey));
            return transaction.CancelTransactionAsync(transactionKey);
        }

        public Task<CreateRefundOut> CreateRefundAsync(string transactionKey, CreateRefundIn input, string apiKey)
        {
            var refund = new ApivtransactionskeyrefundsApi(new ApiClient(apiKey));
            return refund.CreateRefundAsync(transactionKey, input);
        }
    }
}