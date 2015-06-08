namespace Fifthweek.WebJobs.Snapshots
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using Fifthweek.Api.Persistence;
    using Fifthweek.Azure;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Payments.SnapshotCreation;
    using Fifthweek.WebJobs.Shared;

    using CreateSnapshotMessage = Fifthweek.Payments.SnapshotCreation.CreateSnapshotMessage;

    [AutoConstructor]
    public partial class SnapshotProcessor : ISnapshotProcessor
    {
        private readonly ICreateSubscriberSnapshotDbStatement createSubscriberSnapshot;
        private readonly ICreateSubscriberChannelsSnapshotDbStatement createSubscriberChannelsSnapshot;
        private readonly ICreateCreatorChannelsSnapshotDbStatement createCreatorChannelsSnapshot;
        private readonly ICreateCreatorFreeAccessUsersSnapshotDbStatement createCreatorFreeAccessUsersSnapshot;

        public Task CreateSnapshotAsync(
            CreateSnapshotMessage message,
            ICloudStorageAccount cloudStorageAccount,
            ILogger logger,
            CancellationToken cancellationToken)
        {
            try
            {
                switch (message.SnapshotType)
                {
                    case SnapshotType.Subscriber:
                        return this.createSubscriberSnapshot.ExecuteAsync(message.UserId);

                    case SnapshotType.SubscriberChannels:
                        return this.createSubscriberChannelsSnapshot.ExecuteAsync(message.UserId);

                    case SnapshotType.CreatorChannels:
                        return this.createCreatorChannelsSnapshot.ExecuteAsync(message.UserId);

                    case SnapshotType.CreatorFreeAccessUsers:
                        return this.createCreatorFreeAccessUsersSnapshot.ExecuteAsync(message.UserId);
                }

                return Task.FromResult(0);
            }
            catch (Exception t)
            {
                logger.Error(t);
                throw;
            }
        }

        public Task HandlePoisonMessageAsync(
            string message,
            ICloudStorageAccount cloudStorageAccount,
            ILogger logger,
            CancellationToken cancellationToken)
        {
            logger.Warn("Failed to create snapshot for message: {0}", message);
            return Task.FromResult(0);
        }
    }
}