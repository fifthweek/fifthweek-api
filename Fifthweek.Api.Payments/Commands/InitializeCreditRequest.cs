namespace Fifthweek.Api.Payments.Commands
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.Payments.Taxamo;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;

    [AutoConstructor]
    public partial class InitializeCreditRequest : IInitializeCreditRequest
    {
        private readonly IGetUserPaymentOriginDbStatement getUserPaymentOrigin;
        private readonly IDeleteTaxamoTransaction deleteTaxamoTransaction;
        private readonly ICreateTaxamoTransaction createTaxamoTransaction;
        
        public async Task<InitializeApplyCreditRequestResult> HandleAsync(ApplyCreditRequestCommand command)
        {
            command.AssertNotNull("command");

            var origin = await this.getUserPaymentOrigin.ExecuteAsync(command.UserId);

            // Create taxamo transaction.
            var taxamoTransaction = await this.createTaxamoTransaction.ExecuteAsync(
                    command.Amount,
                    origin.BillingCountryCode,
                    origin.CreditCardPrefix,
                    origin.IpAddress,
                    origin.OriginalTaxamoTransactionKey);

            // Verify expected amount matches data returned from taxamo.
            if (taxamoTransaction.TotalAmount.Value != command.ExpectedTotalAmount.Value)
            {
                await this.deleteTaxamoTransaction.ExecuteAsync(taxamoTransaction.Key);
                throw new BadRequestException("The expected total amount did not match the calculated total amount.");
            }

            return new InitializeApplyCreditRequestResult(taxamoTransaction, origin);
        }
    }
}