namespace Fifthweek.Api.Core
{
    using System;

    public class ApiException : Exception
    {
        public ApiException(string message)
            : base(message)
        {
        }

        public ApiException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public ApiException(string message, string reason)
            : base(message)
        {
            this.Reason = reason;
        }

        public ApiException(string message, string reason, Exception innerException)
            : base(message, innerException)
        {
            this.Reason = reason;
        }
        
        public string Reason { get; private set; }
    }
}