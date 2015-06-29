namespace Fifthweek.Api.Availability
{
    using System.Threading.Tasks;

    public interface ITestSqlAzureAvailabilityStatement
    {
        Task<bool> ExecuteAsync();
    }
}