namespace Fifthweek.Api
{
    using System;

    public class BadRequestException : RecoverableException
    {
        public BadRequestException(string message)
            : base(message)
        {
        }
    }
}