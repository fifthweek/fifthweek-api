namespace Fifthweek.Payments.Services.Refunds
{
    using System;

    public class FailedToRefundCreditException : Exception
    {
        public FailedToRefundCreditException(string json, Exception exception)
            : base("Failed to refund credit after performing taxamo refund:" + Environment.NewLine + json, exception)
        {
        }
    }
}