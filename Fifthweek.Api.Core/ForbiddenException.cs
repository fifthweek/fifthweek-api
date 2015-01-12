namespace Fifthweek.Api.Core
{
    using System;

    public class ForbiddenException : Exception
    {
        public ForbiddenException()
        {
        }

        public ForbiddenException(string message)
            : base(message)
        {
        }
    }
}