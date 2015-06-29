namespace Fifthweek.Payments.Shared
{
    using System.Threading.Tasks;

    public interface IRequestProcessPaymentsService
    {
        Task ExecuteAsync();

        Task ExecuteImmediatelyAsync();
    }
}