using System;
using System.Linq;

//// Generated on 25/06/2015 13:32:25 (UTC)
//// Mapped solution in 13.1s


namespace Fifthweek.WebJobs.Payments
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Payments.Services;
    using Fifthweek.Payments.Shared;
    using Fifthweek.Shared;
    using Fifthweek.WebJobs.Shared;
    using Fifthweek.Azure;
    using Microsoft.WindowsAzure.Storage;
    using Microsoft.WindowsAzure.Storage.Blob;
    using System.Net;
    using System.Runtime.ExceptionServices;

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
namespace Fifthweek.WebJobs.Payments
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Payments.Services;
    using Fifthweek.Payments.Shared;
    using Fifthweek.Shared;
    using Fifthweek.WebJobs.Shared;
    using Fifthweek.Azure;
    using Microsoft.WindowsAzure.Storage;
    using Microsoft.WindowsAzure.Storage.Blob;
    using System.Net;

    public partial class PaymentProcessingLease 
    {
        public PaymentProcessingLease(
            Fifthweek.Azure.ICloudStorageAccount cloudStorageAccount,
            System.Threading.CancellationToken cancellationToken)
        {
            if (cloudStorageAccount == null)
            {
                throw new ArgumentNullException("cloudStorageAccount");
            }

            if (cancellationToken == null)
            {
                throw new ArgumentNullException("cancellationToken");
            }

            this.cloudStorageAccount = cloudStorageAccount;
            this.cancellationToken = cancellationToken;
        }
    }
}
namespace Fifthweek.WebJobs.Payments
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Payments.Services;
    using Fifthweek.Payments.Shared;
    using Fifthweek.Shared;
    using Fifthweek.WebJobs.Shared;
    using Fifthweek.Azure;
    using Microsoft.WindowsAzure.Storage;
    using Microsoft.WindowsAzure.Storage.Blob;
    using System.Net;

    public partial class PaymentProcessingLeaseFactory 
    {
        public PaymentProcessingLeaseFactory(
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



