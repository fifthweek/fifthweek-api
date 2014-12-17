namespace Fifthweek.Api.Services
{
    using System.Diagnostics;

    public interface ITraceService
    {
        void Log(TraceLevel level, string message);
    }
}