namespace Fifthweek.WebJobs.Shared
{
    using System;

    using Fifthweek.Logging;
    using Fifthweek.Shared;

    public class ExceptionHandler : IExceptionHandler
    {
        private static readonly IErrorReportingService ReportingService = HardwiredDependencies.NewDefaultReportingService();

        private readonly string webJobIdentifier;

        public ExceptionHandler(string webJobIdentifier)
        {
            this.webJobIdentifier = webJobIdentifier;
        }

        public void ReportExceptionAsync(Exception exception)
        {
            ReportingService.ReportErrorAsync(exception, this.webJobIdentifier, null);
        }

        public void ReportExceptionAsync(Exception exception, string developerName)
        {
            // Developers currently not identified in webjobs. Update this if they ever are.
            ReportingService.ReportErrorAsync(exception, this.webJobIdentifier, null);
        }
    }
}