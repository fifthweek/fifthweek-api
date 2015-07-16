namespace Fifthweek.Payments.Services.Credit
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Shared;

    public interface IInitializeCreditRequest
    {
        Task<InitializeCreditRequestResult> HandleAsync(
            UserId userId,
            PositiveInt amount,
            PositiveInt expectedTotalAmount,
            UserType userType);
    }
}