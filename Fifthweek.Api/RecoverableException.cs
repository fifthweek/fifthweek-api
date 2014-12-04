namespace Fifthweek.Api
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