namespace Fifthweek.Api.Persistence
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using Fifthweek.CodeGeneration;

    [AutoConstructor, AutoEqualityMembers, AutoSql]
    public partial class Collection
    {
        public Collection()
        {
        }

        [Required, Key]
        public Guid Id { get; set; }

        [Required]
        public Guid ChannelId { get; set; }

        [Required, Optional, NonEquatable]
        public Channel Channel { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public DateTime QueueExclusiveLowerBound { get; set; }
    }
}