namespace Fifthweek.WebJobs.Deletions.Shared
{
    using System;

    public class Constants
    {
        public const string GarbageCollectionQueueName = "garbage-collection";

        public static readonly TimeSpan GarbageCollectionMinimumAge = TimeSpan.FromDays(1);
        
        public static readonly TimeSpan GarbageCollectionMessageInitialVisibilityDelay = GarbageCollectionMinimumAge;
    }
}