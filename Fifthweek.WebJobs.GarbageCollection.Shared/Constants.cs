namespace Fifthweek.WebJobs.GarbageCollection.Shared
{
    using System;

    public class Constants
    {
        public const string GarbageCollectionQueueName = "garbage-collection-requests";

        public static readonly TimeSpan GarbageCollectionMinimumAge = TimeSpan.FromDays(1);
        
        public static readonly TimeSpan GarbageCollectionMessageInitialVisibilityDelay = GarbageCollectionMinimumAge;
    }
}