namespace Fifthweek.Api.Persistence.Identity
{
    using System;

    using Microsoft.AspNet.Identity.EntityFramework;

    public class FifthweekRoleStore : RoleStore<FifthweekRole, Guid, FifthweekUserRole>
    {
        public FifthweekRoleStore(FifthweekDbContext context)
            : base(context)
        {
        }
    }
}