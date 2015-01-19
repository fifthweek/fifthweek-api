namespace Fifthweek.Api.Persistence
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Fifthweek.CodeGeneration;

    [AutoConstructor, AutoEqualityMembers, AutoSql]
    public partial class WeeklyReleaseTime
    {
        public WeeklyReleaseTime()
        {
        }

        [Key, Column(Order = 1)]
        public Guid CollectionId { get; set; }

        [Key, Column(Order = 1), Optional, NonEquatable]
        public Collection Collection { get; set; }

        [Key, Column(Order = 2)] // Stored as UTC, starting at Sunday as day 0, to be consistent with .NET's DayOfWeek enum.
        public byte HourOfWeek { get; set; }
    }
}