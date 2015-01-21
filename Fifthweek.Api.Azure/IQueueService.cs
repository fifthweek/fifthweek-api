namespace Fifthweek.Api.Azure
{
    using System;
    using System.Threading.Tasks;

    public interface IQueueService
    {
        Task AddMessageToQueueAsync<TMessage>(string queueName, TMessage messageContent);

        Task AddMessageToQueueAsync<TMessage>(string queueName, TMessage messageContent, TimeSpan? timeToLive, TimeSpan? initialVisibilityDelay);
    }
}