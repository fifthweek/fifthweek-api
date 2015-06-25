namespace Fifthweek.Payments.Services
{
    using System.Threading.Tasks;

    public interface IRequestProcessPaymentsService
    {
        Task ExecuteAsync();
    }
}