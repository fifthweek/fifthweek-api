namespace Fifthweek.Shared
{
    using System;

    public class OptimisticConcurrencyException : Exception
    {
        public OptimisticConcurrencyException()
        {
        }

        public OptimisticConcurrencyException(string message)
            : base(message)
        {
        }
    }
}