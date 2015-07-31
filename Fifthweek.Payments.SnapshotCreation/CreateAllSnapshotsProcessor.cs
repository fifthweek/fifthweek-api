namespace Fifthweek.WebJobs.Payments
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Fifthweek.CodeGeneration;
    using Fifthweek.Payments.SnapshotCreation;

    [AutoConstructor]
    public partial class CreateAllSnapshotsProcessor : ICreateAllSnapshotsProcessor
    {
        private readonly IGetAllStandardUsersDbStatement getAllStandardUsers;
        private readonly ICreateSnapshotMultiplexer createSnapshotMultiplexer;

        public async Task ExecuteAsync()
        {
            var userIds = await this.getAllStandardUsers.ExecuteAsync();
            var snapshotTypes = Enum.GetValues(typeof(SnapshotType)).Cast<SnapshotType>().ToList();

            foreach (var userId in userIds)
            {
                foreach (var snapshotType in snapshotTypes)
                {
                    await this.createSnapshotMultiplexer.ExecuteAsync(userId, snapshotType);
                }
            }
        }
    }
}