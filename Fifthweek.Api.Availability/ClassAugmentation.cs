using System;
using System.Linq;

//// Generated on 11/02/2015 17:39:35 (UTC)
//// Mapped solution in 5.36s


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

    public partial class GetAvailabilityQueryHandler 
    {
        public GetAvailabilityQueryHandler(
            Fifthweek.Api.Core.IExceptionHandler exceptionHandler,
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


