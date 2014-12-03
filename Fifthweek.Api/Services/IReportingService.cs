namespace Fifthweek.Api.Services
{
    using System;
    using System.Threading.Tasks;

    public interface IReportingService
    {
        Task ReportErrorAsync(Exception exception, string identifier);
    }
}