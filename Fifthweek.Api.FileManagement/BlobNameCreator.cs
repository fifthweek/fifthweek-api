namespace Fifthweek.Api.FileManagement
{
    using System;

    public class BlobNameCreator : IBlobNameCreator
    {
        private static readonly Random Random = new Random();

        public string CreateFileName()
        {
            var randomBytes = new byte[4];
            Random.NextBytes(randomBytes);
            var uniqifier = string.Join(string.Empty, randomBytes);
            return DateTime.UtcNow.ToString("yyyy-MM-dd-HH-mm-ss-ffffff") + "-" + uniqifier;
        }
    }
}