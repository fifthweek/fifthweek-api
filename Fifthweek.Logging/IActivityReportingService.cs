namespace Fifthweek.Logging
{
    using System.Threading.Tasks;

    public interface IActivityReportingService
    {
        Task ReportActivityAsync(string activity, Developer developer);
    }
}