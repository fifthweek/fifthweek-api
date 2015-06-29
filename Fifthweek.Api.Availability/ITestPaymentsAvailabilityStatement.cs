namespace Fifthweek.Api.Availability
{
    using System.Threading.Tasks;

    public interface ITestPaymentsAvailabilityStatement
    {
        Task<bool> ExecuteAsync();
    }
}