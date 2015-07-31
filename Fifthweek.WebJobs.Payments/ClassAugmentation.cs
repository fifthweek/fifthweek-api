using System;
using System.Linq;

//// Generated on 31/07/2015 14:33:34 (UTC)
//// Mapped solution in 8.52s


namespace Fifthweek.WebJobs.Payments
{
    using System;
    using System.Linq;
    using System.Net;
    using System.Threading;
    using System.Threading.Tasks;
    using Fifthweek.Azure;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Payments.Shared;
    using Fifthweek.Shared;
    using Fifthweek.WebJobs.Shared;
    using Microsoft.WindowsAzure.Storage;
    using System.Collections.Generic;
    using System.Runtime.ExceptionServices;
    using Fifthweek.Payments.Services;
    using Microsoft.WindowsAzure.Storage.Blob;
    using System.Globalization;
    using Microsoft.WindowsAzure.Storage.Shared.Protocol;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Payments.SnapshotCreation;

    public partial class PaymentProcessingLease 
    {
        public PaymentProcessingLease(
            Fifthweek.Shared.ITimestampCreator timestampCreator,
            Fifthweek.Azure.ICloudStorageAccount cloudStorageAccount,
            System.Threading.CancellationToken cancellationToken)
        {
            if (timestampCreator == null)
            {
                throw new ArgumentNullException("timestampCreator");
            }

            if (cloudStorageAccount == null)
            {
                throw new ArgumentNullException("cloudStorageAccount");
            }

            if (cancellationToken == null)
            {
                throw new ArgumentNullException("cancellationToken");
            }

            this.timestampCreator = timestampCreator;
            this.cloudStorageAccount = cloudStorageAccount;
            this.cancellationToken = cancellationToken;
        }
    }
}
namespace Fifthweek.WebJobs.Payments
{
    using System;
    using System.Linq;
    using System.Net;
    using System.Threading;
    using System.Threading.Tasks;
    using Fifthweek.Azure;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Payments.Shared;
    using Fifthweek.Shared;
    using Fifthweek.WebJobs.Shared;
    using Microsoft.WindowsAzure.Storage;
    using System.Collections.Generic;
    using System.Runtime.ExceptionServices;
    using Fifthweek.Payments.Services;
    using Microsoft.WindowsAzure.Storage.Blob;
    using System.Globalization;
    using Microsoft.WindowsAzure.Storage.Shared.Protocol;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Payments.SnapshotCreation;

    public partial class PaymentProcessingLeaseFactory 
    {
        public PaymentProcessingLeaseFactory(
            Fifthweek.Shared.ITimestampCreator timestampCreator,
            Fifthweek.Azure.ICloudStorageAccount cloudStorageAccount)
        {
            if (timestampCreator == null)
            {
                throw new ArgumentNullException("timestampCreator");
            }

            if (cloudStorageAccount == null)
            {
                throw new ArgumentNullException("cloudStorageAccount");
            }

            this.timestampCreator = timestampCreator;
            this.cloudStorageAccount = cloudStorageAccount;
        }
    }
}
namespace Fifthweek.WebJobs.Payments
{
    using System;
    using System.Linq;
    using System.Net;
    using System.Threading;
    using System.Threading.Tasks;
    using Fifthweek.Azure;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Payments.Shared;
    using Fifthweek.Shared;
    using Fifthweek.WebJobs.Shared;
    using Microsoft.WindowsAzure.Storage;
    using System.Collections.Generic;
    using System.Runtime.ExceptionServices;
    using Fifthweek.Payments.Services;
    using Microsoft.WindowsAzure.Storage.Blob;
    using System.Globalization;
    using Microsoft.WindowsAzure.Storage.Shared.Protocol;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Payments.SnapshotCreation;

    public partial class PaymentProcessor 
    {
        public PaymentProcessor(
            Fifthweek.Payments.Services.IProcessAllPayments processAllPayments,
            Fifthweek.WebJobs.Payments.IPaymentProcessingLeaseFactory paymentProcessingLeaseFactory,
            Fifthweek.Payments.Shared.IRequestProcessPaymentsService requestProcessPayments)
        {
            if (processAllPayments == null)
            {
                throw new ArgumentNullException("processAllPayments");
            }

            if (paymentProcessingLeaseFactory == null)
            {
                throw new ArgumentNullException("paymentProcessingLeaseFactory");
            }

            if (requestProcessPayments == null)
            {
                throw new ArgumentNullException("requestProcessPayments");
            }

            this.processAllPayments = processAllPayments;
            this.paymentProcessingLeaseFactory = paymentProcessingLeaseFactory;
            this.requestProcessPayments = requestProcessPayments;
        }
    }
}
namespace Fifthweek.WebJobs.Payments
{
    using System;
    using System.Linq;
    using System.Net;
    using System.Threading;
    using System.Threading.Tasks;
    using Fifthweek.Azure;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Payments.Shared;
    using Fifthweek.Shared;
    using Fifthweek.WebJobs.Shared;
    using Microsoft.WindowsAzure.Storage;
    using System.Collections.Generic;
    using System.Runtime.ExceptionServices;
    using Fifthweek.Payments.Services;
    using Microsoft.WindowsAzure.Storage.Blob;
    using System.Globalization;
    using Microsoft.WindowsAzure.Storage.Shared.Protocol;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Payments.SnapshotCreation;

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



