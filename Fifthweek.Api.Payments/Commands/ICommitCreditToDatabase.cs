namespace Fifthweek.Api.Payments.Commands
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Payments.Taxamo;

    public interface ICommitCreditToDatabase
    {
        Task HandleAsync(UserId userId, TaxamoTransactionResult taxamoTransaction, UserPaymentOriginResult origin, StripeTransactionResult stripeTransaction);
    }
}