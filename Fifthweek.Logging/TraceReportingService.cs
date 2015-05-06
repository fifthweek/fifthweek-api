namespace Fifthweek.Logging
{
    using System;
    using System.Diagnostics;
    using System.Threading.Tasks;

    public class TraceReportingService : IReportingService
    {
        public Task ReportErrorAsync(Exception t, string identifier, Developer developer)
        {
            Trace.TraceError("Error Identifier: " + identifier);

            if (developer != null)
            {
                Trace.TraceError("Developer: " + developer.GitName);
            }

            Trace.TraceError(t.ToString());
            return Task.FromResult(false);
        }

        public Task ReportActivityAsync(string activity, Developer developer)
        {
            if (developer != null)
            {
                Trace.TraceInformation("Developer: " + developer.GitName);
            }

            Trace.TraceInformation(activity);
            return Task.FromResult(false);
        }
    }
}