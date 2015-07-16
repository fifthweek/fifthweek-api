namespace Fifthweek.Payments.Services.Credit.Taxamo
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Fifthweek.CodeGeneration;
    using Fifthweek.Payments.Taxamo;
    using Fifthweek.Shared;

    using global::Taxamo.Model;

    [AutoConstructor]
    public partial class CreateTaxamoTransaction : ICreateTaxamoTransaction
    {
        public const string CustomId = "Credit";

        private readonly ITaxamoApiKeyRepository taxamoApiKeyRepository;
        private readonly ITaxamoService taxamoService;

        public async Task<TaxamoTransactionResult> ExecuteAsync(
            PositiveInt amount,
            string billingCountryCode,
            string creditCardPrefix,
            string ipAddress,
            string originalTaxamoTransactionKey,
            UserType userType)
        {
            amount.AssertNotNull("amount");

            var apiKey = this.taxamoApiKeyRepository.GetApiKey(userType);

            var input = new CreateTransactionIn
            { 
                Transaction = new InputTransaction 
                {
                    CurrencyCode = PaymentConstants.Usd,
                    TransactionLines = new List<InputTransactionLine>
                    {
                        new InputTransactionLine
                        {
                            CustomId = CustomId,
                            Amount = new AmountInUsCents(amount.Value).ToUsDollars()
                        }
                    },
                    BuyerCreditCardPrefix = creditCardPrefix,
                    BuyerIp = ipAddress,
                    BillingCountryCode = billingCountryCode,
                    OriginalTransactionKey = originalTaxamoTransactionKey
                }
            };

            var result = await this.taxamoService.CreateTransactionAsync(input, apiKey);

            return new TaxamoTransactionResult(
                result.Transaction.Key,
                AmountInUsCents.FromAmountInUsDollars(result.Transaction.Amount.Value),
                AmountInUsCents.FromAmountInUsDollars(result.Transaction.TotalAmount.Value),
                AmountInUsCents.FromAmountInUsDollars(result.Transaction.TaxAmount.Value),
                result.Transaction.TransactionLines[0].TaxRate,
                result.Transaction.TransactionLines[0].TaxName,
                result.Transaction.TaxEntityName,
                result.Transaction.CountryName);
        }
    }
}