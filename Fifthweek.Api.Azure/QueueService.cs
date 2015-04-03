namespace Fifthweek.Api.Azure
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Core;
    using Fifthweek.Azure;
    using Fifthweek.Shared;

    using Microsoft.WindowsAzure.Storage.Queue;

    using Newtonsoft.Json;

    public class QueueService : IQueueService
    {
        private readonly ICloudStorageAccount cloudStorageAccount;

        public QueueService(ICloudStorageAccount cloudStorageAccount)
        {
            this.cloudStorageAccount = cloudStorageAccount;
        }

        public Task AddMessageToQueueAsync<TMessage>(string queueName, TMessage messageContent)
        {
            return this.AddMessageToQueueAsync(queueName, messageContent, null, null);
        }

        public async Task AddMessageToQueueAsync<TMessage>(string queueName, TMessage messageContent, TimeSpan? timeToLive, TimeSpan? initialVisibilityDelay)
        {
            queueName.AssertNotNull("queueName");
            messageContent.AssertNotNull("messageContent");

            var cloudQueueClient = this.cloudStorageAccount.CreateCloudQueueClient();
            var queue = cloudQueueClient.GetQueueReference(queueName);

            var serializedMessageContent = JsonConvert.SerializeObject(messageContent);
            var message = new CloudQueueMessage(serializedMessageContent);

            await queue.AddMessageAsync(message, timeToLive, initialVisibilityDelay);
        }
    }
}