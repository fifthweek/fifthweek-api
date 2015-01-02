namespace Fifthweek.Api.Core
{
    using System.Diagnostics;

    public interface ITraceService
    {
        void Log(TraceLevel level, string message);
    }
}