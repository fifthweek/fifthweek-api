namespace Fifthweek.Api.Payments.Commands
{
    using System;

    public class FailedToApplyCreditException : Exception
    {
        public FailedToApplyCreditException(string json, Exception exception)
            : base("Failed to apply credit after performing stripe charge:" + Environment.NewLine + json, exception)
        {
        }
    }
}