namespace Fifthweek.Api.Persistence
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Shared;

    [AutoConstructor, AutoEqualityMembers, AutoSql]
    public partial class Subscription
    {
        public Subscription()
        {
        }

        [Required, Key]
        public Guid Id { get; set; }

        [Required, Index]
        public Guid CreatorId { get; set; }

        [Required, Optional, NonEquatable]
        public FifthweekUser Creator { get; set; }

        [Required]
        [MaxLength(25)] // See: ValidSubscriptionName.MaxLength
        public string Name { get; set; }

        [Required]
        [MaxLength(55)] // See: ValidTagline.MaxLength
        public string Tagline { get; set; }

        [Required]
        [MaxLength(250)] // See: ValidIntroduction.MaxLength
        public string Introduction { get; set; }

        [Optional]
        [MaxLength(2000)] // See: ValidDescription.MaxLength
        public string Description { get; set; }

        [Optional]
        [MaxLength(100)] // See: ValidExternalVideoUrl.MaxLength
        public string ExternalVideoUrl { get; set; }

        [Optional]
        public Guid? HeaderImageFileId { get; set; }

        [Optional, NonEquatable]
        public File HeaderImageFile { get; set; }

        [Required]
        public DateTime CreationDate { get; set; }
    }
}