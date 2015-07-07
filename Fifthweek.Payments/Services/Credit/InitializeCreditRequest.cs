namespace Fifthweek.Payments.Services.Credit
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Payments.Services.Credit.Taxamo;
    using Fifthweek.Shared;

    [AutoConstructor]
    public partial class InitializeCreditRequest : IInitializeCreditRequest
    {
        private readonly IGetUserPaymentOriginDbStatement getUserPaymentOrigin;
        private readonly IDeleteTaxamoTransaction deleteTaxamoTransaction;
        private readonly ICreateTaxamoTransaction createTaxamoTransaction;
        
        public async Task<InitializeCreditRequestResult> HandleAsync(
            UserId userId,
            PositiveInt amount,
            PositiveInt expectedTotalAmount)
        {
            userId.AssertNotNull("userId");
            amount.AssertNotNull("amount");

            var origin = await this.getUserPaymentOrigin.ExecuteAsync(userId);

            // Create taxamo transaction.
            var taxamoTransaction = await this.createTaxamoTransaction.ExecuteAsync(
                    amount,
                    origin.BillingCountryCode,
                    origin.CreditCardPrefix,
                    origin.IpAddress,
                    origin.OriginalTaxamoTransactionKey);

            // Verify expected amount matches data returned from taxamo.
            if (expectedTotalAmount != null && taxamoTransaction.TotalAmount.Value != expectedTotalAmount.Value)
            {
                await this.deleteTaxamoTransaction.ExecuteAsync(taxamoTransaction.Key);
                throw new BadRequestException("The expected total amount did not match the calculated total amount.");
            }

            return new InitializeCreditRequestResult(taxamoTransaction, origin);
        }
    }
}