namespace Fifthweek.Api.Persistence.Identity
{
    using System;

    using Microsoft.AspNet.Identity.EntityFramework;

    public class FifthweekRole : IdentityRole<Guid, FifthweekUserRole>
    {
        public static string Administrator = "administrator";

        public static string Creator = "creator";
    }
}