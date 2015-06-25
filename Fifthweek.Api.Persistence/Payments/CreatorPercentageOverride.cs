namespace Fifthweek.Api.Persistence.Payments
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.CodeGeneration;

    [AutoConstructor, AutoEqualityMembers, AutoSql]
    public partial class CreatorPercentageOverride
    {
        public CreatorPercentageOverride()
        {
        }

        // Not a foreign key as we need this to calculate historic payments even if user is
        // deleted. We could add a timestamp here as well and insert new overrides to better
        // support historic calculations, but in reality it won't be of much use as these
        // change infrequently and historic calculations are rarely further back than a couple
        // of weeks, so I've deferred doing more until later.
        [Required, Key]
        public Guid UserId { get; set; }

        [Required]
        public decimal Percentage { get; set; }

        [Optional]
        public DateTime? ExpiryDate { get; set; }
    }
}