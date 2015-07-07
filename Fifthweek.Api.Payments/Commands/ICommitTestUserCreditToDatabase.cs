namespace Fifthweek.Api.Payments.Commands
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Shared;

    public interface ICommitTestUserCreditToDatabase
    {
        Task HandleAsync(
            UserId userId,
            PositiveInt amount);
    }
}