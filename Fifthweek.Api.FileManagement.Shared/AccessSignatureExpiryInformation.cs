namespace Fifthweek.Api.FileManagement.Shared
{
    using System;

    public class AccessSignatureExpiryInformation
    {
        public AccessSignatureExpiryInformation(DateTime @public, DateTime @private)
        {
            this.Public = @public;
            this.Private = @private;
        }

        public DateTime Public { get; private set; }

        public DateTime Private { get; private set; }
    }
}
