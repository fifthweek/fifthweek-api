using System;
using System.Linq;

//// Generated on 26/06/2015 16:45:35 (UTC)
//// Mapped solution in 15.92s


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
    using System.Globalization;
    using System.Threading.Tasks;
    using Fifthweek.Api.Availability.Queries;
    using Fifthweek.Api.Core;
    using Fifthweek.Azure;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Payments.Shared;
    using Fifthweek.Shared;
    using Microsoft.Practices.EnterpriseLibrary.TransientFaultHandling;

    public partial class TestPaymentsAvailabilityStatement 
    {
        public TestPaymentsAvailabilityStatement(
            IExceptionHandler exceptionHandler,
            Microsoft.Practices.EnterpriseLibrary.TransientFaultHandling.ITransientErrorDetectionStrategy transientErrorDetectionStrategy,
            Fifthweek.Azure.ICloudStorageAccount cloudStorageAccount,
            Fifthweek.Shared.ITimestampCreator timestampCreator,
            Fifthweek.Payments.Shared.IRequestProcessPaymentsService requestProcessPayments)
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

            this.exceptionHandler = exceptionHandler;
            this.transientErrorDetectionStrategy = transientErrorDetectionStrategy;
            this.cloudStorageAccount = cloudStorageAccount;
            this.timestampCreator = timestampCreator;
            this.requestProcessPayments = requestProcessPayments;
        }
    }
}
namespace Fifthweek.Api.Availability
{
    using System;
    using System.Threading.Tasks;
    using Fifthweek.Api.Availability.Queries;
    using Fifthweek.Api.Core;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;

    using Microsoft.Practices.EnterpriseLibrary.TransientFaultHandling;

    public partial class TestSqlAzureAvailabilityStatement 
    {
        public TestSqlAzureAvailabilityStatement(
            IExceptionHandler exceptionHandler,
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


