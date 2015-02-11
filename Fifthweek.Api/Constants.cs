namespace Fifthweek.Api
{
    using System;
    using System.Net.Mail;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.Logging;
    using Fifthweek.Api.SendGrid;

    public static class Constants
    {
        public const string AdministratorUsers = "Admin";

        public static readonly TimeSpan AccessTokenExpiryTime = TimeSpan.FromMinutes(30);

        // This is used for reporting errors, as we can't rely on AutoFac being in a good state.
        public static readonly IDeveloperRepository DefaultDeveloperRepository = new DeveloperRepository();

        // This is used for reporting errors, as we can't rely on AutoFac being in a good state.
        public static readonly ISendEmailService DefaultSendEmailService = new SendGridEmailService();

        // This is used for reporting errors, as we can't rely on AutoFac being in a good state.
        public static readonly IReportingService DefaultReportingService = System.Diagnostics.Debugger.IsAttached
            ? (IReportingService)new AggregateReportingService(new TraceReportingService())
            : (IReportingService)new AggregateReportingService(new TraceReportingService(), new EmailReportingService(DefaultSendEmailService), new SlackReportingService());
    }
}