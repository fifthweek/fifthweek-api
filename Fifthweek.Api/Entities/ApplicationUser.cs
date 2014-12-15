using Microsoft.AspNet.Identity.EntityFramework;

namespace Fifthweek.Api.Entities
{
    using System;

    public class ApplicationUser : IdentityUser
    {
        public string ExampleWork { get; set; }

        public DateTime RegistrationDate { get; set; }

        public DateTime LastSignInDate { get; set; }

        public DateTime LastAccessTokenDate { get; set; }
    }
}