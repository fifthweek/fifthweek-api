using System;
using System.Linq;

//// Generated on 07/06/2015 17:39:56 (UTC)
//// Mapped solution in 18.88s


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

    public partial class SnapshotProcessor 
    {
        public SnapshotProcessor(
            ICreateSubscriberSnapshotDbStatement createSubscriberSnapshot,
            ICreateSubscriberChannelsSnapshotDbStatement createSubscriberChannelsSnapshot,
            ICreateCreatorChannelsSnapshotDbStatement createCreatorChannelsSnapshot,
            ICreateCreatorFreeAccessUsersSnapshotDbStatement createCreatorFreeAccessUsersSnapshot)
        {
            if (createSubscriberSnapshot == null)
            {
                throw new ArgumentNullException("createSubscriberSnapshot");
            }

            if (createSubscriberChannelsSnapshot == null)
            {
                throw new ArgumentNullException("createSubscriberChannelsSnapshot");
            }

            if (createCreatorChannelsSnapshot == null)
            {
                throw new ArgumentNullException("createCreatorChannelsSnapshot");
            }

            if (createCreatorFreeAccessUsersSnapshot == null)
            {
                throw new ArgumentNullException("createCreatorFreeAccessUsersSnapshot");
            }

            this.createSubscriberSnapshot = createSubscriberSnapshot;
            this.createSubscriberChannelsSnapshot = createSubscriberChannelsSnapshot;
            this.createCreatorChannelsSnapshot = createCreatorChannelsSnapshot;
            this.createCreatorFreeAccessUsersSnapshot = createCreatorFreeAccessUsersSnapshot;
        }
    }
}



