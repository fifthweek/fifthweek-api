namespace Fifthweek.Payments.Services.Refunds.Taxamo
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Fifthweek.CodeGeneration;
    using Fifthweek.Payments.Services.Credit;
    using Fifthweek.Payments.Services.Credit.Taxamo;
    using Fifthweek.Payments.Taxamo;
    using Fifthweek.Shared;

    using global::Taxamo.Model;

    [AutoConstructor]
    public partial class CreateTaxamoRefund : ICreateTaxamoRefund
    {
        private readonly ITaxamoApiKeyRepository taxamoApiKeyRepository;
        private readonly ITaxamoService taxamoService;

        public async Task<TaxamoRefundResult> ExecuteAsync(
            string taxamoTransactionKey, 
            PositiveInt refundCreditAmount, 
            UserType userType)
        {
            taxamoTransactionKey.AssertNotNull("taxamoTransactionKey");
            refundCreditAmount.AssertNotNull("refundCreditAmount");

            var amount = AmountInMinorDenomination.Create(refundCreditAmount).ToMajorDenomination();

            var apiKey = this.taxamoApiKeyRepository.GetApiKey(userType);
          
            var input = new CreateRefundIn
            {
                Amount = amount,
                CustomId = CreateTaxamoTransaction.CustomId,
            };

            var result = await this.taxamoService.CreateRefundAsync(taxamoTransactionKey, input, apiKey);

            var totalAmount = AmountInMinorDenomination.FromMajorDenomination(result.TotalAmount.Value);
            var taxAmount = AmountInMinorDenomination.FromMajorDenomination(result.TaxAmount.Value);
            return new TaxamoRefundResult(totalAmount.ToPositiveInt(), taxAmount.ToNonNegativeInt());
        }

        [AutoConstructor, AutoEqualityMembers]
        public partial class TaxamoRefundResult
        {
            public PositiveInt TotalRefundAmount { get; private set; }

            public NonNegativeInt TaxRefundAmount { get; private set; }
        }
    }
}