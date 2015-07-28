namespace Fifthweek.Api.Persistence.Payments
{
    public enum LedgerTransactionType
    {
        CreditAddition = 0,
        SubscriptionPayment = 1,
        CreditRefund = 2,
        SubscriptionRefund = 3,
    }
}