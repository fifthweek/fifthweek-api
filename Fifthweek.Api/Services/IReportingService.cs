namespace Fifthweek.Api.Services
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Models;

    public interface IReportingService
    {
        Task ReportErrorAsync(Exception exception, string identifier, Developer developer);
    }
}