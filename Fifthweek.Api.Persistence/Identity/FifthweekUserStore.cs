namespace Fifthweek.Api.Persistence.Identity
{
    using System;

    using Microsoft.AspNet.Identity.EntityFramework;

    public class FifthweekUserStore :
        UserStore<FifthweekUser, FifthweekRole, Guid, FifthweekUserLogin, FifthweekUserRole, FifthweekUserClaim>
    {
        public FifthweekUserStore(FifthweekDbContext context)
            : base(context)
        {
        }
    }
}