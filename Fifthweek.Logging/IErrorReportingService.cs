namespace Fifthweek.Logging
{
    using System;
    using System.Threading.Tasks;

    public interface IErrorReportingService
    {
        Task ReportErrorAsync(Exception exception, string identifier, Developer developer);
    }
}