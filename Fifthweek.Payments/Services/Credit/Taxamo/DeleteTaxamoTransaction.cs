namespace Fifthweek.Payments.Services.Credit.Taxamo
{
    using System.Threading.Tasks;

    using Fifthweek.CodeGeneration;
    using Fifthweek.Payments.Taxamo;
    using Fifthweek.Shared;

    [AutoConstructor]
    public partial class DeleteTaxamoTransaction : IDeleteTaxamoTransaction
    {
        private readonly ITaxamoApiKeyRepository taxamoApiKeyRepository;
        private readonly ITaxamoService taxamoService;

        public async Task ExecuteAsync(string transactionKey, UserType userType)
        {
            transactionKey.AssertNotNull("transactionKey");

            var apiKey = this.taxamoApiKeyRepository.GetApiKey(userType);
            await this.taxamoService.CancelTransactionAsync(transactionKey, apiKey);
        }
    }
}