namespace Fifthweek.Payments.SnapshotCreation
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;

    [AutoConstructor]
    public partial class CreateSnapshotMultiplexer : ICreateSnapshotMultiplexer
    {
        private readonly ICreateSubscriberSnapshotDbStatement createSubscriberSnapshot;
        private readonly ICreateSubscriberChannelsSnapshotDbStatement createSubscriberChannelsSnapshot;
        private readonly ICreateCreatorChannelsSnapshotDbStatement createCreatorChannelsSnapshot;
        private readonly ICreateCreatorFreeAccessUsersSnapshotDbStatement createCreatorFreeAccessUsersSnapshot;
        
        public Task ExecuteAsync(UserId userId, SnapshotType snapshotType)
        {
            switch (snapshotType)
            {
                case SnapshotType.Subscriber:
                    return this.createSubscriberSnapshot.ExecuteAsync(userId);

                case SnapshotType.SubscriberChannels:
                    return this.createSubscriberChannelsSnapshot.ExecuteAsync(userId);

                case SnapshotType.CreatorChannels:
                    return this.createCreatorChannelsSnapshot.ExecuteAsync(userId);

                case SnapshotType.CreatorFreeAccessUsers:
                    return this.createCreatorFreeAccessUsersSnapshot.ExecuteAsync(userId);
            }

            throw new InvalidOperationException("Unknown snapshot type: " + snapshotType);
        }
    }
}