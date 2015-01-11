namespace Fifthweek.Api.Persistence.Identity
{
    using System;

    using Microsoft.AspNet.Identity.EntityFramework;

    public class FifthweekRole : IdentityRole<Guid, FifthweekUserRole>
    {
        public static string Administrator = "administrator";

        public static string Creator = "creator";

        public static Guid AdministratorId = Guid.Parse("{A7E8660E-62B9-47F5-996E-6260BDC7FE7C}");

        public static Guid CreatorId = Guid.Parse("{1B753E28-6522-4373-BEB6-ABE5C1C166C0}");
    }
}