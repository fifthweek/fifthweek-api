using System;
using System.Linq;

//// Generated on 06/08/2015 09:23:12 (UTC)
//// Mapped solution in 9.16s


namespace Fifthweek.WebJobs.Payments
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.ExceptionServices;
    using System.Threading;
    using System.Threading.Tasks;

    using Fifthweek.Azure;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Payments.Services;
    using Fifthweek.Payments.Shared;
    using Fifthweek.Shared;
    using Fifthweek.WebJobs.Shared;
    using Microsoft.WindowsAzure.Storage.Blob;

    public partial class PaymentProcessor 
    {
        public PaymentProcessor(
            Fifthweek.Payments.Services.IProcessAllPayments processAllPayments,
            IBlobLeaseFactory blobLeaseFactory,
            Fifthweek.Payments.Shared.IRequestProcessPaymentsService requestProcessPayments)
        {
            if (processAllPayments == null)
            {
                throw new ArgumentNullException("processAllPayments");
            }

            if (blobLeaseFactory == null)
            {
                throw new ArgumentNullException("blobLeaseFactory");
            }

            if (requestProcessPayments == null)
            {
                throw new ArgumentNullException("requestProcessPayments");
            }

            this.processAllPayments = processAllPayments;
            this.blobLeaseFactory = blobLeaseFactory;
            this.requestProcessPayments = requestProcessPayments;
        }
    }
}
namespace Fifthweek.WebJobs.Payments
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
            Fifthweek.Payments.SnapshotCreation.ICreateSnapshotMultiplexer createSnapshotMultiplexer,
            Fifthweek.WebJobs.Payments.ICreateAllSnapshotsProcessor createAllSnapshots)
        {
            if (createSnapshotMultiplexer == null)
            {
                throw new ArgumentNullException("createSnapshotMultiplexer");
            }

            if (createAllSnapshots == null)
            {
                throw new ArgumentNullException("createAllSnapshots");
            }

            this.createSnapshotMultiplexer = createSnapshotMultiplexer;
            this.createAllSnapshots = createAllSnapshots;
        }
    }
}



