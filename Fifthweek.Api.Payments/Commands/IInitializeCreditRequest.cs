namespace Fifthweek.Api.Payments.Commands
{
    using System.Threading.Tasks;

    public interface IInitializeCreditRequest
    {
        Task<InitializeApplyCreditRequestResult> HandleAsync(ApplyCreditRequestCommand command);
    }
}