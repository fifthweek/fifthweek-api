using System;
using System.Linq;

//// Generated on 09/07/2015 10:21:49 (UTC)
//// Mapped solution in 53.54s


namespace Fifthweek.Api.Availability.Controllers
{
    using System;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using System.Web.Http;
    using System.Web.Http.Description;
    using Fifthweek.Api.Availability.Queries;
    using Fifthweek.Api.Core;
    using Fifthweek.CodeGeneration;

    public partial class AvailabilityController 
    {
        public AvailabilityController(
            Fifthweek.Api.Core.IQueryHandler<Fifthweek.Api.Availability.Queries.GetAvailabilityQuery,Fifthweek.Api.Availability.AvailabilityResult> getAvailability)
        {
            if (getAvailability == null)
            {
                throw new ArgumentNullException("getAvailability");
            }

            this.getAvailability = getAvailability;
        }
    }
}
namespace Fifthweek.Api.Availability.Queries
{
    using System;
    using System.Linq;
    using System.ComponentModel;
    using System.Data.Entity;
    using System.Threading.Tasks;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;
    using Microsoft.Practices.EnterpriseLibrary.TransientFaultHandling;
    using Fifthweek.Shared;

    public partial class GetAvailabilityQueryHandler 
    {
        public GetAvailabilityQueryHandler(
            Fifthweek.Api.Availability.ITestSqlAzureAvailabilityStatement testSqlAzureAvailability,
            Fifthweek.Api.Availability.ITestPaymentsAvailabilityStatement testPaymentsAvailability)
        {
            if (testSqlAzureAvailability == null)
            {
                throw new ArgumentNullException("testSqlAzureAvailability");
            }

            if (testPaymentsAvailability == null)
            {
                throw new ArgumentNullException("testPaymentsAvailability");
            }

            this.testSqlAzureAvailability = testSqlAzureAvailability;
            this.testPaymentsAvailability = testPaymentsAvailability;
        }
    }
}
namespace Fifthweek.Api.Availability
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.CodeGeneration;
    using System.Globalization;
    using Fifthweek.Api.Availability.Queries;
    using Fifthweek.Api.Core;
    using Fifthweek.Azure;
    using Fifthweek.Payments.Shared;
    using Fifthweek.Shared;
    using Microsoft.Practices.EnterpriseLibrary.TransientFaultHandling;

    public partial class CountUsersDbStatement 
    {
        public CountUsersDbStatement(
            Fifthweek.Api.Persistence.IFifthweekDbConnectionFactory connectionFactory)
        {
            if (connectionFactory == null)
            {
                throw new ArgumentNullException("connectionFactory");
            }

            this.connectionFactory = connectionFactory;
        }
    }
}
namespace Fifthweek.Api.Availability
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.CodeGeneration;
    using System.Globalization;
    using Fifthweek.Api.Availability.Queries;
    using Fifthweek.Api.Core;
    using Fifthweek.Azure;
    using Fifthweek.Payments.Shared;
    using Fifthweek.Shared;
    using Microsoft.Practices.EnterpriseLibrary.TransientFaultHandling;

    public partial class TestPaymentsAvailabilityStatement 
    {
        public TestPaymentsAvailabilityStatement(
            Fifthweek.Shared.IExceptionHandler exceptionHandler,
            Microsoft.Practices.EnterpriseLibrary.TransientFaultHandling.ITransientErrorDetectionStrategy transientErrorDetectionStrategy,
            Fifthweek.Azure.ICloudStorageAccount cloudStorageAccount,
            ITimestampCreator timestampCreator,
            Fifthweek.Payments.Shared.IRequestProcessPaymentsService requestProcessPayments,
            Fifthweek.Api.Availability.ILastPaymentsRestartTimeContainer lastPaymentsRestartTimeContainer)
        {
            if (exceptionHandler == null)
            {
                throw new ArgumentNullException("exceptionHandler");
            }

            if (transientErrorDetectionStrategy == null)
            {
                throw new ArgumentNullException("transientErrorDetectionStrategy");
            }

            if (cloudStorageAccount == null)
            {
                throw new ArgumentNullException("cloudStorageAccount");
            }

            if (timestampCreator == null)
            {
                throw new ArgumentNullException("timestampCreator");
            }

            if (requestProcessPayments == null)
            {
                throw new ArgumentNullException("requestProcessPayments");
            }

            if (lastPaymentsRestartTimeContainer == null)
            {
                throw new ArgumentNullException("lastPaymentsRestartTimeContainer");
            }

            this.exceptionHandler = exceptionHandler;
            this.transientErrorDetectionStrategy = transientErrorDetectionStrategy;
            this.cloudStorageAccount = cloudStorageAccount;
            this.timestampCreator = timestampCreator;
            this.requestProcessPayments = requestProcessPayments;
            this.lastPaymentsRestartTimeContainer = lastPaymentsRestartTimeContainer;
        }
    }
}
namespace Fifthweek.Api.Availability
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.CodeGeneration;
    using System.Globalization;
    using Fifthweek.Api.Availability.Queries;
    using Fifthweek.Api.Core;
    using Fifthweek.Azure;
    using Fifthweek.Payments.Shared;
    using Fifthweek.Shared;
    using Microsoft.Practices.EnterpriseLibrary.TransientFaultHandling;

    public partial class TestSqlAzureAvailabilityStatement 
    {
        public TestSqlAzureAvailabilityStatement(
            Fifthweek.Shared.IExceptionHandler exceptionHandler,
            Microsoft.Practices.EnterpriseLibrary.TransientFaultHandling.ITransientErrorDetectionStrategy transientErrorDetectionStrategy,
            Fifthweek.Api.Availability.ICountUsersDbStatement countUsersDbStatement)
        {
            if (exceptionHandler == null)
            {
                throw new ArgumentNullException("exceptionHandler");
            }

            if (transientErrorDetectionStrategy == null)
            {
                throw new ArgumentNullException("transientErrorDetectionStrategy");
            }

            if (countUsersDbStatement == null)
            {
                throw new ArgumentNullException("countUsersDbStatement");
            }

            this.exceptionHandler = exceptionHandler;
            this.transientErrorDetectionStrategy = transientErrorDetectionStrategy;
            this.countUsersDbStatement = countUsersDbStatement;
        }
    }
}


