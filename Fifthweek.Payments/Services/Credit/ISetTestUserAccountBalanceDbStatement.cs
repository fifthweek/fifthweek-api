namespace Fifthweek.Payments.Services.Credit
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Shared;

    public interface ISetTestUserAccountBalanceDbStatement
    {
        Task ExecuteAsync(UserId userId, DateTime timestamp, PositiveInt amount);
    }
}