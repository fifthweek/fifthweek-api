namespace Fifthweek.Api.Persistence.Identity
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using Fifthweek.CodeGeneration;

    using Microsoft.AspNet.Identity.EntityFramework;

    [LiftMembers, AutoEqualityMembers, AutoSql(Table = "AspNetUserRoles")]
    public partial class FifthweekUserRole : IdentityUserRole<Guid>
    {
        public FifthweekUserRole()
        {
        }

        private class LiftedMembers : IdentityUserRole<Guid>
        {
            [Key]
            public override Guid RoleId { get; set; }

            [Key]
            public override Guid UserId { get; set; }
        }
    }
}