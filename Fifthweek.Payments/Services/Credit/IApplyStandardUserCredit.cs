namespace Fifthweek.Payments.Services.Credit
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Shared;

    public interface IApplyStandardUserCredit
    {
        Task ExecuteAsync(UserId userId, PositiveInt amount, PositiveInt expectedTotalAmount);
    }
}