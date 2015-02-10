namespace Fifthweek.Api.Persistence.Identity
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using Fifthweek.CodeGeneration;

    using Microsoft.AspNet.Identity.EntityFramework;

    [LiftMembers, AutoEqualityMembers, AutoSql(Table = "AspNetRoles")]
    public partial class FifthweekRole : IdentityRole<Guid, FifthweekUserRole>
    {
        public static readonly string Administrator = "administrator";
        public static readonly Guid AdministratorId = Guid.Parse("{A7E8660E-62B9-47F5-996E-6260BDC7FE7C}");

        public static readonly string PreRelease = "pre-release";
        public static readonly Guid PreReleaseId = Guid.Parse("{E7DB6386-373A-4E84-A18F-BAC22BBA5CC9}");

        public static readonly string Creator = "creator";
        public static readonly Guid CreatorId = Guid.Parse("{1B753E28-6522-4373-BEB6-ABE5C1C166C0}");

        public FifthweekRole()
        {
        }

        private class LiftedMembers
        {
            [Key]
            public Guid Id { get; set; }

            public string Name { get; set; }
        }
    }
}