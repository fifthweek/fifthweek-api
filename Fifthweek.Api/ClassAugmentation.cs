using System;
using System.Linq;

//// Generated on 06/05/2015 14:24:45 (UTC)
//// Mapped solution in 9s


namespace Fifthweek.Api
{
    using System;
    using System.Linq;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Fifthweek.Api.Core;
    using Fifthweek.CodeGeneration;
    using Microsoft.Owin.Security.OAuth;
    using Fifthweek.Logging;

    public partial class ExceptionHandler 
    {
        public ExceptionHandler(
            Fifthweek.Api.Core.IRequestContext requestContext)
        {
            if (requestContext == null)
            {
                throw new ArgumentNullException("requestContext");
            }

            this.requestContext = requestContext;
        }
    }
}
namespace Fifthweek.Api
{
    using System;
    using System.Linq;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Fifthweek.Api.Core;
    using Fifthweek.CodeGeneration;
    using Microsoft.Owin.Security.OAuth;
    using Fifthweek.Logging;

    public partial class FifthweekActivityReporter 
    {
        public FifthweekActivityReporter(
            Fifthweek.Logging.IActivityReportingService activityReporter,
            Fifthweek.Api.Core.IRequestContext requestContext,
            Fifthweek.Logging.IDeveloperRepository developerRepository,
            Fifthweek.Api.Core.IExceptionHandler exceptionHandler)
        {
            if (activityReporter == null)
            {
                throw new ArgumentNullException("activityReporter");
            }

            if (requestContext == null)
            {
                throw new ArgumentNullException("requestContext");
            }

            if (developerRepository == null)
            {
                throw new ArgumentNullException("developerRepository");
            }

            if (exceptionHandler == null)
            {
                throw new ArgumentNullException("exceptionHandler");
            }

            this.activityReporter = activityReporter;
            this.requestContext = requestContext;
            this.developerRepository = developerRepository;
            this.exceptionHandler = exceptionHandler;
        }
    }
}



