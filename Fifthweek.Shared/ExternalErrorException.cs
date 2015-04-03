namespace Fifthweek.Shared
{
    using System;

    public class ExternalErrorException : Exception
    {
        public ExternalErrorException(string message)
            : base(message)
        {
        }
    }
}