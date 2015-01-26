namespace Fifthweek.Api.Core
{
    using System;

    public class RetriesOccuredException : Exception
    {
        public RetriesOccuredException(string message)
            : base(message)
        {
        }
    }
}