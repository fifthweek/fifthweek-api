namespace Fifthweek.Api.FileManagement.Shared
{
    using System;

    public class Constants
    {
        // If you're thinking about moving this to the Fifthweek.Shared project, consider using IBlobLocationGenerator instead.
        public const string PublicFileBlobContainerName = "public";

        public const string PostFeedImageThumbnailName = "feed";
        public const string PostPreviewImageThumbnailName = "post-preview";

        public static readonly TimeSpan WriteSignatureTimeSpan = TimeSpan.FromHours(1);
        public static readonly TimeSpan PrivateReadSignatureTimeSpan = TimeSpan.FromHours(1);
        public static readonly TimeSpan PublicReadSignatureTimeSpan = TimeSpan.FromDays(7);
        
        // This should match the client.  The client will try and refresh access signatures when they are this close to expiring.
        public static readonly TimeSpan ReadSignatureMinimumExpiryTime = TimeSpan.FromMinutes(10);
    }
}