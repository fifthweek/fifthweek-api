namespace Fifthweek.Api.Identity.OAuth
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Security.Cryptography;

    using Fifthweek.Shared;

    public class AesEncryptionService : IAesEncryptionService
    {
        private const int InitializationVectorLength = 16;
        private static readonly byte[] EmptyInitializationVector = Enumerable.Repeat<byte>(0, InitializationVectorLength).ToArray();
        private static readonly byte[] Key = Convert.FromBase64String(Fifthweek.Api.Core.Constants.RefreshTokenEncryptionKey);

        public byte[] Encrypt(byte[] input, bool useEmptyInitializationVector)
        {
            input.AssertNotNull("input");

            using (var aes = Aes.Create())
            {
                aes.Key = Key;

                if (useEmptyInitializationVector)
                {
                    if (input.Length != (aes.BlockSize / 8))
                    {
                        throw new InvalidOperationException(
                            "Only input data equal to block size is supported in this mode.");
                    }

                    aes.Mode = CipherMode.ECB;
                    aes.Padding = PaddingMode.None;
                    aes.IV = EmptyInitializationVector;
                }

                using (var outputStream = new MemoryStream())
                {
                    using (var encryptor = aes.CreateEncryptor())
                    using (var cryptoStream = new CryptoStream(outputStream, encryptor, CryptoStreamMode.Write))
                    using (var binaryWriter = new BinaryWriter(cryptoStream))
                    {
                        binaryWriter.Write(input);
                    }

                    var output = outputStream.ToArray();

                    var encrypted = useEmptyInitializationVector ? output : aes.IV.Concat(output).ToArray();
                    return encrypted;
                }
            }
        }

        public byte[] Decrypt(byte[] input, bool useEmptyInitializationVector)
        {
            input.AssertNotNull("input");

            var initializationVector = useEmptyInitializationVector ? EmptyInitializationVector : input.Take(InitializationVectorLength).ToArray();
            input = useEmptyInitializationVector ? input : input.Skip(InitializationVectorLength).ToArray();

            using (var aes = Aes.Create())
            {
                aes.Key = Key;
                aes.IV = initializationVector;

                if (useEmptyInitializationVector)
                {
                    aes.Mode = CipherMode.ECB;
                    aes.Padding = PaddingMode.None;
                }

                using (var outputStream = new MemoryStream())
                {
                    using (var decryptor = aes.CreateDecryptor())
                    using (var inputStream = new MemoryStream(input))
                    using (var cryptoStream = new CryptoStream(inputStream, decryptor, CryptoStreamMode.Read))
                    {
                        cryptoStream.CopyTo(outputStream);
                    }

                    var output = outputStream.ToArray();

                    return output;
                }
            }
        }
    }
}