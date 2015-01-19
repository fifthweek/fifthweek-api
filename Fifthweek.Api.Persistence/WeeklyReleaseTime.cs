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

        [Key, Column(Order = 2)]
        public byte DayOfWeek { get; set; }

        [Key, Column(Order = 3)]
        public TimeSpan TimeOfDay { get; set; }
    }
}