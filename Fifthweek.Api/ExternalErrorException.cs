namespace Fifthweek.Api
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