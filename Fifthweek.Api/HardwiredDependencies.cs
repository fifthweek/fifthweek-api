namespace Fifthweek.Api
{
    using System;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.EndToEndTestMailboxes;
    using Fifthweek.Api.Logging;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.SendGrid;

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
            return new EndToEndTestSendEmailServiceDecorator(
                new SendGridEmailService(),
                new SetLatestMessageDbStatement(
                    new FifthweekDbConnectionFactory()));
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