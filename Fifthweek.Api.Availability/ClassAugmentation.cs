using System;
using System.Linq;

//// Generated on 10/02/2015 12:14:25 (UTC)
//// Mapped solution in 15.81s


namespace Fifthweek.Api.Availability.Controllers
{
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
            Fifthweek.Api.Persistence.IFifthweekDbContext fifthweekDbContext,
            Fifthweek.Api.Core.IExceptionHandler exceptionHandler,
            Microsoft.Practices.EnterpriseLibrary.TransientFaultHandling.ITransientErrorDetectionStrategy transientErrorDetectionStrategy)
        {
            if (fifthweekDbContext == null)
            {
                throw new ArgumentNullException("fifthweekDbContext");
            }

            if (exceptionHandler == null)
            {
                throw new ArgumentNullException("exceptionHandler");
            }

            if (transientErrorDetectionStrategy == null)
            {
                throw new ArgumentNullException("transientErrorDetectionStrategy");
            }

            this.fifthweekDbContext = fifthweekDbContext;
            this.exceptionHandler = exceptionHandler;
            this.transientErrorDetectionStrategy = transientErrorDetectionStrategy;
        }
    }
}


