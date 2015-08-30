namespace Fifthweek.Api.Identity.OAuth
{
    public interface IAesEncryptionService
    {
        byte[] Encrypt(byte[] input, bool useEmptyInitializationVector);

        byte[] Decrypt(byte[] input, bool useEmptyInitializationVector);
    }
}