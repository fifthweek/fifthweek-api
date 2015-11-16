namespace Fifthweek.Api.Posts.Shared
{
    using System;

    public interface IGetFreePostTimestamp
    {
        DateTime Execute(DateTime input);
    }
}