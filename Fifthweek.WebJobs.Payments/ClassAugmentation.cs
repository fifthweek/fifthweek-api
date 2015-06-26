using System;
using System.Linq;

//// Generated on 25/06/2015 17:45:43 (UTC)
//// Mapped solution in 7.67s


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

    public partial class PaymentProcessor 
    {
        public PaymentProcessor(
            Fifthweek.Payments.Services.IProcessAllPayments processAllPayments,
            Fifthweek.WebJobs.Payments.IPaymentProcessingLeaseFactory paymentProcessingLeaseFactory,
            Fifthweek.Payments.Services.IRequestProcessPaymentsService requestProcessPayments)
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



