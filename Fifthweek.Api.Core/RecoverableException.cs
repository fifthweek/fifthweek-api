namespace Fifthweek.Api.Core
{
    using System;

    public class RecoverableException : Exception
    {
        public RecoverableException(string message)
            : base(message)
        {
        }
    }
}