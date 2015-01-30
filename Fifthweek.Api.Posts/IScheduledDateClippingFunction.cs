namespace Fifthweek.Api.Posts
{
    using System;

    public interface IScheduledDateClippingFunction
    {
        DateTime Apply(DateTime now, DateTime? scheduled);
    }
}