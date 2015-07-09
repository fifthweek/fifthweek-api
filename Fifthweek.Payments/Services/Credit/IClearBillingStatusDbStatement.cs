namespace Fifthweek.Payments.Services.Credit
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Identity.Shared.Membership;

    public interface IClearBillingStatusDbStatement
    {
        Task ExecuteAsync(UserId userId);
    }
}