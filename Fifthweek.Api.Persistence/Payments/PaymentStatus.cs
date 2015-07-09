namespace Fifthweek.Api.Persistence.Payments
{
    public enum PaymentStatus
    {
        None = 0,
        Retry1 = 1,
        Retry2 = 2,
        Retry3 = 3,
        Failed = 4,
    }
}