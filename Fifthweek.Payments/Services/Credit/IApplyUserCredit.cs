namespace Fifthweek.Payments.Services.Credit
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Payments.Shared;
    using Fifthweek.Shared;

    public interface IApplyUserCredit
    {
        Task ExecuteAsync(
            UserId userId,
            DateTime timestamp,
            TransactionReference transactionReference,
            PositiveInt amount, 
            PositiveInt expectedTotalAmount, 
            UserType userType);
    }
}