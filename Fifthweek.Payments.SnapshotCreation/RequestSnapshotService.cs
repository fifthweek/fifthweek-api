namespace Fifthweek.Payments.SnapshotCreation
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Azure;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Azure;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;

    [AutoConstructor]
    public partial class RequestSnapshotService : IRequestSnapshotService
    {
        public static readonly TimeSpan SnapshotDelay = TimeSpan.FromSeconds(30);

        private readonly IQueueService queueService;

        public Task ExecuteAsync(UserId userId, SnapshotType snapshotType)
        {
            userId.AssertNotNull("userId");

            return this.queueService.AddMessageToQueueAsync(
                Shared.Constants.RequestSnapshotQueueName,
                new CreateSnapshotMessage(userId, snapshotType),
                null,
                SnapshotDelay);
        }
    }
}