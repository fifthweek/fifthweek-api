namespace Fifthweek.Api.Persistence
{
    using System;

    using Microsoft.AspNet.Identity.EntityFramework;

    public class ApplicationUser : IdentityUser
    {
        public string ExampleWork { get; set; }

        public DateTime RegistrationDate { get; set; }

        public DateTime LastSignInDate { get; set; }

        public DateTime LastAccessTokenDate { get; set; }
    }
}