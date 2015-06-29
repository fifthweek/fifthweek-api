namespace Fifthweek.Api.Availability.Queries
{
    using System;

    public class TransientErrorException : Exception
    {
        public TransientErrorException(string message, Exception exception)
            : base(message, exception)
        {
        }
    }
}