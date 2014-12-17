namespace Fifthweek.Api.Services
{
    using System;
    using System.Diagnostics;
    using System.Threading.Tasks;

    public class TraceReportingService : IReportingService
    {
        public Task ReportErrorAsync(Exception t, string identifier)
        {
            Trace.TraceError("Error Identifier: " + identifier);
            Trace.TraceError(t.ToString());
            return Task.FromResult(false);
        }
    }
}