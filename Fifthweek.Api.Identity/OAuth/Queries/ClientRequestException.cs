namespace Fifthweek.Api.Identity.OAuth.Queries
{
    using System;

    public class ClientRequestException : Exception
    {
        public ClientRequestException(string message)
            : base(message)
        {
        }
    }
}