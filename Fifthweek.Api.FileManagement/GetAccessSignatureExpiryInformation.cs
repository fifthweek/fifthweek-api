namespace Fifthweek.Api.FileManagement
{
    using System;

    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.CodeGeneration;

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

            var expiry = this.RoundUp(now, baseTimeSpan);

            if ((expiry - now) <= Constants.ReadSignatureMinimumExpiryTime)
            {
                expiry = expiry.Add(baseTimeSpan);
            }

            return expiry;
        }

        private DateTime RoundUp(DateTime dt, TimeSpan d)
        {
            if (dt.Kind != DateTimeKind.Utc)
            {
                throw new InvalidOperationException("Expiry time must be in UTC");
            }

            return new DateTime(((dt.Ticks + d.Ticks - 1) / d.Ticks) * d.Ticks, DateTimeKind.Utc);
        }

    }
}