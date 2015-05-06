namespace Fifthweek.Api.Core
{
    using System.Threading.Tasks;

    public interface IFifthweekActivityReporter
    {
        void ReportActivityInBackground(string activity);

        Task ReportActivityAsync(string activity);
    }
}