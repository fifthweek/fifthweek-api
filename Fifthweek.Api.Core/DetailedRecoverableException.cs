namespace Fifthweek.Api.Core
{
    using System;

    using Fifthweek.Shared;

    public class DetailedRecoverableException : RecoverableException
    {
        private readonly string detailedMessage;

        public DetailedRecoverableException(string userMessage, string detailedMessage)
            : base(userMessage)
        {
            this.detailedMessage = detailedMessage;
        }

        public string DetailedMessage
        {
            get
            {
                return this.detailedMessage;
            }
        }

        public override string ToString()
        {
            return this.detailedMessage + Environment.NewLine + base.ToString();
        }
    }
}