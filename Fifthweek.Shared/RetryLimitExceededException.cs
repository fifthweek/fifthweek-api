namespace Fifthweek.Shared
{
    using System;

    public class RetryLimitExceededException : Exception
    {
        public RetryLimitExceededException(string message, Exception exception)
            : base(message, exception)
        {
        }    
    }
}