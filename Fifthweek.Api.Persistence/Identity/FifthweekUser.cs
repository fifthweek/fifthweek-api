namespace Fifthweek.Api.Persistence.Identity
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.AspNet.Identity.EntityFramework;

    /// <summary>
    /// See here for same code for changing the PK type for identity tables.
    /// https://aspnet.codeplex.com/SourceControl/latest#Samples/Identity/ChangePK/PrimaryKeysConfigTest/Models/IdentityModels.cs
    /// </summary>
    public class FifthweekUser : IdentityUser<Guid, FifthweekUserLogin, FifthweekUserRole, FifthweekUserClaim>
    {
        public string ExampleWork { get; set; }

        public DateTime RegistrationDate { get; set; }

        public DateTime LastSignInDate { get; set; }

        public DateTime LastAccessTokenDate { get; set; }
    }
}
