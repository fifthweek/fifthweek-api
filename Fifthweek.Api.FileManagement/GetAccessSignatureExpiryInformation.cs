namespace Fifthweek.Api.FileManagement
{
    using System;

    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;

    using Constants = Fifthweek.Api.FileManagement.Shared.Constants;

    [AutoConstructor]
    public partial class GetAccessSignatureExpiryInformation : IGetAccessSignatureExpiryInformation
    {
        public AccessSignatureExpiryInformation Execute(DateTime now)
        {
            return new AccessSignatureExpiryInformation(
                this.GetNextExpiry(now, true),
                this.GetNextExpiry(now, false));
        }

        internal DateTime GetNextExpiry(DateTime now, bool isPublic)
        {
            var baseTimeSpan
                = isPublic
                ? Constants.PublicReadSignatureTimeSpan
                : Constants.PrivateReadSignatureTimeSpan;

            var expiry = DateTimeUtils.RoundUp(now, baseTimeSpan);

            if ((expiry - now) <= Constants.ReadSignatureMinimumExpiryTime)
            {
                expiry = expiry.Add(baseTimeSpan);
            }

            return expiry;
        }
    }
}