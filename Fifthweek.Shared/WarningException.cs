namespace Fifthweek.Shared
{
    using System;

    public class WarningException : Exception
    {
        public WarningException(string message)
            : base(message)
        {
        }
    }
}