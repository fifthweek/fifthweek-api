namespace Fifthweek.Payments.SnapshotCreation
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Azure;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Payments.Shared;

    [AutoConstructor]
    public partial class RequestSnapshotService : IRequestSnapshotService
    {
        public static readonly TimeSpan SnapshotDelay = TimeSpan.FromSeconds(30);

        private readonly IQueueService queueService;

        public Task ExecuteAsync(UserId userId, SnapshotType snapshotType)
        {
            return this.queueService.AddMessageToQueueAsync(
                Constants.RequestSnapshotQueueName,
                new CreateSnapshotMessage(userId, snapshotType),
                null,
                SnapshotDelay);
        }
    }
}