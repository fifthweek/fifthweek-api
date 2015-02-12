namespace Fifthweek.Api.Core
{
    using System;

    public class UnauthenticatedException : Exception
    {
        public UnauthenticatedException()
        {
        }

        public UnauthenticatedException(string message)
            : base(message)
        {
        }

        public UnauthenticatedException(string format, params object[] args)
            : base(string.Format(format, args))
        {
        }
    }
}