using System;
using System.Linq;

//// Generated on 06/08/2015 09:41:51 (UTC)
//// Mapped solution in 9.39s


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
            Fifthweek.Azure.ICloudStorageAccount cloudStorageAccount,
            System.Threading.CancellationToken cancellationToken,
            System.String leaseObjectName)
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

            if (leaseObjectName == null)
            {
                throw new ArgumentNullException("leaseObjectName");
            }

            this.timestampCreator = timestampCreator;
            this.cloudStorageAccount = cloudStorageAccount;
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



