namespace Fifthweek.Api.Persistence.Identity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Fifthweek.CodeGeneration;

    using Microsoft.AspNet.Identity.EntityFramework;

    /// <summary>
    /// See here for same code for changing the PK type for identity tables.
    /// https://aspnet.codeplex.com/SourceControl/latest#Samples/Identity/ChangePK/PrimaryKeysConfigTest/Models/IdentityModels.cs
    /// </summary>
    [LiftMembers, AutoEqualityMembers, AutoSql(Table = "AspNetUsers")]
    public partial class FifthweekUser : IdentityUser<Guid, FifthweekUserLogin, FifthweekUserRole, FifthweekUserClaim>
    {
        public FifthweekUser()
        {
        }

        public string ExampleWork { get; set; }

        public DateTime RegistrationDate { get; set; }

        public DateTime LastSignInDate { get; set; }

        public DateTime LastAccessTokenDate { get; set; }

        [Optional, NonEquatable]
        public File ProfileImageFile { get; set; }

        public Guid? ProfileImageFileId { get; set; }

        [Index(IsUnique = true)]
        public override string Email
        {
            get
            {
                return base.Email;
            }

            set
            {
                base.Email = value;
            }
        }

        [Optional]
        [MaxLength(25)] // See: ValidBlogName.MaxLength - default blog name is creator's name.
        public string Name { get; set; }

        // Inherit IdentityUser and override members to provide a little extra safety in case a base member ever got renamed.
        private class LiftedMembers : IdentityUser<Guid, FifthweekUserLogin, FifthweekUserRole, FifthweekUserClaim>
        {
            [Key]
            public override Guid Id { get; set; }

            public override int AccessFailedCount { get; set; }

            public override bool EmailConfirmed { get; set; }

            public override bool LockoutEnabled { get; set; }

            public override DateTime? LockoutEndDateUtc { get; set; }

            public override string PasswordHash { get; set; }

            public override string PhoneNumber { get; set; }

            public override bool PhoneNumberConfirmed { get; set; }

            public override string SecurityStamp { get; set; }

            public override bool TwoFactorEnabled { get; set; }

            public override string UserName { get; set; }
        }
    }
}
