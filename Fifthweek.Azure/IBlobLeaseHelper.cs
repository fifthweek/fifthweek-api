namespace Fifthweek.Azure
{
    using System;

    public interface IBlobLeaseHelper
    {
        ICloudBlockBlob GetLeaseBlob(string leaseObjectName);

        bool IsLeaseConflictException(Exception t);
    }
}