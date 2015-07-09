namespace Fifthweek.Api.Availability
{
    using System;

    public interface ILastPaymentsRestartTimeContainer
    {
        DateTime LastRestartTime { get; set; }
    }
}