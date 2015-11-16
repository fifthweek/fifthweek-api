namespace Fifthweek.Api.Posts
{
    using System;

    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.Shared;

    public class GetFreePostTimestamp : IGetFreePostTimestamp
    {
        private static readonly TimeSpan FreePostInterval = TimeSpan.FromDays(7);

        public DateTime Execute(DateTime input)
        {
            var result = DateTimeUtils.RoundUp(input, FreePostInterval);

            if (result != input)
            {
                return result.Subtract(FreePostInterval);
            }

            return result;
        }
    }
}