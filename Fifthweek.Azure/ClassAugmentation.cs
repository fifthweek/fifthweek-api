using System;
using System.Linq;

//// Generated on 06/08/2015 18:08:23 (UTC)
//// Mapped solution in 16.24s


namespace Fifthweek.Azure
{
    using System;
    using System.Globalization;
    using System.Net;
    using System.Threading;
    using System.Threading.Tasks;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
    using Microsoft.WindowsAzure.Storage;
    using System.Linq;

    public partial class BlobLease 
    {
        public BlobLease(
            Fifthweek.Shared.ITimestampCreator timestampCreator,
            Fifthweek.Azure.IBlobLeaseHelper blobLeaseHelper,
            System.Threading.CancellationToken cancellationToken,
            System.String leaseObjectName)
        {
            if (timestampCreator == null)
            {
                throw new ArgumentNullException("timestampCreator");
            }

            if (blobLeaseHelper == null)
            {
                throw new ArgumentNullException("blobLeaseHelper");
            }

            if (cancellationToken == null)
            {
                throw new ArgumentNullException("cancellationToken");
            }

            if (leaseObjectName == null)
            {
                throw new ArgumentNullException("leaseObjectName");
            }

            this.timestampCreator = timestampCreator;
            this.blobLeaseHelper = blobLeaseHelper;
            this.cancellationToken = cancellationToken;
            this.leaseObjectName = leaseObjectName;
        }
    }
}
namespace Fifthweek.Azure
{
    using System.Threading;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
    using System;
    using System.Linq;
    using System.Globalization;
    using System.Net;
    using System.Threading.Tasks;
    using Microsoft.WindowsAzure.Storage;

    public partial class BlobLeaseFactory 
    {
        public BlobLeaseFactory(
            Fifthweek.Shared.ITimestampCreator timestampCreator,
            Fifthweek.Azure.IBlobLeaseHelper blobLeaseHelper)
        {
            if (timestampCreator == null)
            {
                throw new ArgumentNullException("timestampCreator");
            }

            if (blobLeaseHelper == null)
            {
                throw new ArgumentNullException("blobLeaseHelper");
            }

            this.timestampCreator = timestampCreator;
            this.blobLeaseHelper = blobLeaseHelper;
        }
    }
}
namespace Fifthweek.Azure
{
    using System;
    using System.Net;
    using System.Threading.Tasks;
    using Fifthweek.CodeGeneration;
    using Microsoft.WindowsAzure.Storage;
    using System.Linq;
    using System.Globalization;
    using System.Threading;
    using Fifthweek.Shared;

    public partial class BlobLeaseHelper 
    {
        public BlobLeaseHelper(
            Fifthweek.Azure.ICloudStorageAccount cloudStorageAccount)
        {
            if (cloudStorageAccount == null)
            {
                throw new ArgumentNullException("cloudStorageAccount");
            }

            this.cloudStorageAccount = cloudStorageAccount;
        }
    }
}



