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
    }
}