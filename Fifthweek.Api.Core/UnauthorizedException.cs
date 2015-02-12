namespace Fifthweek.Api.Core
{
    using System;

    public class UnauthorizedException : Exception
    {
        public UnauthorizedException()
        {
        }

        public UnauthorizedException(string message)
            : base(message)
        {
        }

        public UnauthorizedException(string format, params object[] args)
            : base(string.Format(format, args))
        {
        }
    }
}