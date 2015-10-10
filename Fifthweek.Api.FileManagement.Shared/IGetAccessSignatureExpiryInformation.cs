namespace Fifthweek.Api.FileManagement.Shared
{
    using System;

    public interface IGetAccessSignatureExpiryInformation
    {
        AccessSignatureExpiryInformation Execute(DateTime now);
    }
}