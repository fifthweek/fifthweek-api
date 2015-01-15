namespace Fifthweek.Api.Azure
{
    public interface ICloudQueueClient
    {
        ICloudQueue GetQueueReference(string queueName);
    }
}