namespace Fifthweek.Payments.Services
{
    using System.Threading.Tasks;

    using Fifthweek.Azure;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;

    using Newtonsoft.Json;

    [AutoConstructor]
    public partial class PersistPaymentProcessingDataStatement : IPersistPaymentProcessingDataStatement
    {
        private readonly ICloudStorageAccount cloudStorageAccount;

        public async Task ExecuteAsync(PersistedPaymentProcessingData data)
        {
            using (PaymentsPerformanceLogger.Instance.Log(typeof(PersistPaymentProcessingDataStatement)))
            {
                data.AssertNotNull("data");

                var client = this.cloudStorageAccount.CreateCloudBlobClient();
                var container = client.GetContainerReference(Fifthweek.Payments.Shared.Constants.PaymentProcessingDataContainerName);
                var blob = container.GetBlockBlobReference(data.Id.ToString());
                var jsonData = JsonConvert.SerializeObject(data);
                await blob.UploadTextAsync(jsonData);
            }
        }
    }
}