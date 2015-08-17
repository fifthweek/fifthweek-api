namespace Fifthweek.Api.Blogs
{
    using System;

    using Fifthweek.Shared;

    public static class Extensions
    {
        public static DateTime ReleasableRevenueDate(this ITimestampCreator timestampCreator)
        {
            var now = timestampCreator.Now();
            return now.AddDays(-BlogsConstants.ReleaseableRevenueDays);
        }
    }
}