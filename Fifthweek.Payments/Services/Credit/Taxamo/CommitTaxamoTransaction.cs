namespace Fifthweek.Payments.Services.Credit.Taxamo
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.CodeGeneration;
    using Fifthweek.Payments.Taxamo;
    using Fifthweek.Shared;

    using global::Taxamo.Model;

    [AutoConstructor]
    public partial class CommitTaxamoTransaction : ICommitTaxamoTransaction
    {
        private readonly ITaxamoApiKeyRepository taxamoApiKeyRepository;
        private readonly ITaxamoService taxamoService;

        public async Task ExecuteAsync(
            TaxamoTransactionResult taxamoTransactionResult,
            StripeTransactionResult stripeTransactionResult,
            UserType userType)
        {
            taxamoTransactionResult.AssertNotNull("taxamoTransactionResult");
            stripeTransactionResult.AssertNotNull("stripeTransactionResult");

            var apiKey = this.taxamoApiKeyRepository.GetApiKey(userType);
            var input = new CreatePaymentIn 
            {
                Amount = taxamoTransactionResult.Amount.ToUsDollars(),
                PaymentTimestamp = stripeTransactionResult.Timestamp.ToIso8601String(),
                PaymentInformation = string.Format(
                    "Reference:{0}, StripeChargeId:{1}", 
                    stripeTransactionResult.TransactionReference, 
                    stripeTransactionResult.StripeChargeId),
            };

            await this.taxamoService.CreatePaymentAsync(taxamoTransactionResult.Key, input, apiKey);
        }
    }
}