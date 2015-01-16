namespace Fifthweek.Azure
{
    public interface ICloudQueueClient
    {
        ICloudQueue GetQueueReference(string queueName);
    }
}