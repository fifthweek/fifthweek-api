namespace Fifthweek.Api.Services
{
    using System.Diagnostics;

    public class TraceService : ITraceService
    {
        public void Log(TraceLevel level, string message)
        {
            switch (level)
            {
                case TraceLevel.Verbose:
                    Trace.WriteLine(message);
                    break;

                case TraceLevel.Info:
                    Trace.TraceInformation(message);
                    break;

                case TraceLevel.Warning:
                    Trace.TraceWarning(message);
                    break;

                case TraceLevel.Error:
                    Trace.TraceError(message);
                    break;
            }
        }
    }
}