using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fifthweek.WebJobs.Shared
{
    using Fifthweek.Api.SendGrid;
    using Fifthweek.Logging;
    using Fifthweek.Shared;

    public static class HardwiredDependencies
    {
        public static readonly TimeSpan AccessTokenExpiryTime = TimeSpan.FromMinutes(30);

        // This is used for reporting errors, as we can't rely on AutoFac being in a good state.
        public static IDeveloperRepository NewDefaultDeveloperRepository()
        {
            return new DeveloperRepository();
        }

        // This is used for reporting errors, as we can't rely on AutoFac being in a good state.
        public static ISendEmailService NewDefaultSendEmailService()
        {
            return new SendGridEmailService();
        }

        // This is used for reporting errors, as we can't rely on AutoFac being in a good state.
        public static IReportingService NewDefaultReportingService()
        {
            return System.Diagnostics.Debugger.IsAttached
               ? (IReportingService)new AggregateReportingService(new TraceReportingService())
               : (IReportingService)new AggregateReportingService(new TraceReportingService(), new EmailReportingService(NewDefaultSendEmailService()), new SlackReportingService());
        }
    }
}
