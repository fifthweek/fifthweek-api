namespace Fifthweek.Api.Core
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