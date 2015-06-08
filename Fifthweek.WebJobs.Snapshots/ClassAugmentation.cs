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
    using Fifthweek.Payments.Services;
    using Fifthweek.WebJobs.Shared;

    public partial class SnapshotProcessor 
    {
        public SnapshotProcessor(
            Fifthweek.Payments.Services.ICreateSubscriberSnapshotDbStatement createSubscriberSnapshot,
            Fifthweek.Payments.Services.ICreateSubscriberChannelSnapshotDbStatement createSubscriberChannelSnapshot,
            Fifthweek.Payments.Services.ICreateCreatorChannelSnapshotDbStatement createCreatorChannelSnapshot,
            Fifthweek.Payments.Services.ICreateCreatorGuestListSnapshotDbStatement createCreatorGuestListSnapshot)
        {
            if (createSubscriberSnapshot == null)
            {
                throw new ArgumentNullException("createSubscriberSnapshot");
            }

            if (createSubscriberChannelSnapshot == null)
            {
                throw new ArgumentNullException("createSubscriberChannelSnapshot");
            }

            if (createCreatorChannelSnapshot == null)
            {
                throw new ArgumentNullException("createCreatorChannelSnapshot");
            }

            if (createCreatorGuestListSnapshot == null)
            {
                throw new ArgumentNullException("createCreatorGuestListSnapshot");
            }

            this.createSubscriberSnapshot = createSubscriberSnapshot;
            this.createSubscriberChannelSnapshot = createSubscriberChannelSnapshot;
            this.createCreatorChannelSnapshot = createCreatorChannelSnapshot;
            this.createCreatorGuestListSnapshot = createCreatorGuestListSnapshot;
        }
    }
}



