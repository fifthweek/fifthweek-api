namespace Fifthweek.Payments.Services.Credit
{
    using System;

    public class StripeChargeFailedException : Exception
    {
        public StripeChargeFailedException(Exception exception)
            : base("Failed to create stripe charge.", exception)
        {
        }
    }
}