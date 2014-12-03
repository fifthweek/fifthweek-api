namespace Fifthweek.Api.Services
{
    using System;
    using System.Diagnostics;
    using System.Threading.Tasks;

    public class TraceReportingService : IReportingService
    {
        public Task ReportErrorAsync(Exception t, string identifier)
        {
            Trace.WriteLine("Error Identifier: " + identifier);
            Trace.WriteLine(t.ToString());
            return Task.FromResult(false);
        }
    }
}