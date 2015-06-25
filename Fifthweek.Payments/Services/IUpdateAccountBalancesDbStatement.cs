namespace Fifthweek.Payments.Services
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Identity.Shared.Membership;

    public interface IUpdateAccountBalancesDbStatement
    {
        Task ExecuteAsync(UserId userId, DateTime timestamp);
    }
}