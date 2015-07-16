namespace Fifthweek.Payments.Services.Credit.Taxamo
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Fifthweek.CodeGeneration;
    using Fifthweek.Payments.Taxamo;
    using Fifthweek.Shared;

    using global::Taxamo.Api;
    using global::Taxamo.Client;
    using global::Taxamo.Model;

    [AutoConstructor]
    public partial class GetTaxInformation : IGetTaxInformation
    {
        private readonly ITaxamoApiKeyRepository taxamoApiKeyRepository;
        private readonly ITaxamoService taxamoService;

        public async Task<TaxamoCalculationResult> ExecuteAsync(
            PositiveInt amount,
            string billingCountryCode,
            string creditCardPrefix,
            string ipAddress,
            string originalTaxamoTransactionKey,
            UserType userType)
        {
            amount.AssertNotNull("amount");

            var apiKey = this.taxamoApiKeyRepository.GetApiKey(userType);

            var input = new CalculateTaxIn 
            { 
                Transaction = new InputTransaction 
                {
                    CurrencyCode = PaymentConstants.Usd,
                    TransactionLines = new List<InputTransactionLine>
                    {
                        new InputTransactionLine
                        {
                            CustomId = CreateTaxamoTransaction.CustomId,
                            Amount = new AmountInUsCents(amount.Value).ToUsDollars()
                        }
                    },
                    BuyerCreditCardPrefix = creditCardPrefix,
                    BuyerIp = ipAddress,
                    BillingCountryCode = billingCountryCode,
                    OriginalTransactionKey = originalTaxamoTransactionKey
                }
            };

            var result = await this.taxamoService.CalculateTaxAsync(input, apiKey);

            var countryDetected = this.IsCountryDetected(result);
            
            return new TaxamoCalculationResult(
                AmountInUsCents.FromAmountInUsDollars(result.Transaction.Amount.Value),
                AmountInUsCents.FromAmountInUsDollars(result.Transaction.TotalAmount.Value),
                AmountInUsCents.FromAmountInUsDollars(result.Transaction.TaxAmount.Value),
                result.Transaction.TransactionLines[0].TaxRate,
                result.Transaction.TransactionLines[0].TaxName,
                result.Transaction.TaxEntityName,
                countryDetected ? result.Transaction.CountryName : null,
                countryDetected ? null : this.GetPossibleCountries(result));
        }

        private bool IsCountryDetected(CalculateTaxOut data)
        {
            int count = 0;
            if (data.Transaction.Evidence.ByCc != null
                && data.Transaction.Evidence.ByCc.Used.HasValue
                && data.Transaction.Evidence.ByCc.Used.Value)
            {
                ++count;
            }

            if (data.Transaction.Evidence.ByIp != null
                && data.Transaction.Evidence.ByIp.Used.HasValue
                && data.Transaction.Evidence.ByIp.Used.Value)
            {
                ++count;
            }

            if (data.Transaction.Evidence.ByBilling != null
                && data.Transaction.Evidence.ByBilling.Used.HasValue
                && data.Transaction.Evidence.ByBilling.Used.Value)
            {
                ++count;
            }

            return count > 1;
        }

        private List<TaxamoCalculationResult.PossibleCountry> GetPossibleCountries(CalculateTaxOut data)
        {
            var result = new List<TaxamoCalculationResult.PossibleCountry>();
            if (data.Transaction.Countries.ByCc != null
                && data.Transaction.Countries.ByCc.Name != null)
            {
                result.Add(new TaxamoCalculationResult.PossibleCountry(
                    data.Transaction.Countries.ByCc.Name,
                    data.Transaction.Countries.ByCc.Code));
            }

            if (data.Transaction.Countries.ByIp != null
                && data.Transaction.Countries.ByIp.Name != null)
            {
                result.Add(new TaxamoCalculationResult.PossibleCountry(
                    data.Transaction.Countries.ByIp.Name,
                    data.Transaction.Countries.ByIp.Code));
            }

            if (data.Transaction.Countries.ByBilling != null
                && data.Transaction.Countries.ByBilling.Name != null)
            {
                result.Add(new TaxamoCalculationResult.PossibleCountry(
                    data.Transaction.Countries.ByBilling.Name,
                    data.Transaction.Countries.ByBilling.Code));
            }

            return result;
        }
    }
}