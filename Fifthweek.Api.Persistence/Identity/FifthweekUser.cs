using Fifthweek.Api.Core;

namespace Fifthweek.Api.Persistence.Identity
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNet.Identity.EntityFramework;

    /// <summary>
    /// See here for same code for changing the PK type for identity tables.
    /// https://aspnet.codeplex.com/SourceControl/latest#Samples/Identity/ChangePK/PrimaryKeysConfigTest/Models/IdentityModels.cs
    /// </summary>
    [AutoEqualityMembers]
    public partial class FifthweekUser : IdentityUser<Guid, FifthweekUserLogin, FifthweekUserRole, FifthweekUserClaim>, IIdentityEquatable
    {
        public string ExampleWork { get; set; }

        public DateTime RegistrationDate { get; set; }

        public DateTime LastSignInDate { get; set; }

        public DateTime LastAccessTokenDate { get; set; }

        [Optional]
        public File ProfileImageFile { get; set; }

        public Guid ProfileImageFileId { get; set; }

        public bool IdentityEquals(object other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            if (other.GetType() != this.GetType())
            {
                return false;
            }

            return this.IdentityEquals((FifthweekUser)other);
        }

        protected bool IdentityEquals(FifthweekUser other)
        {
            return this.Id.Equals(other.Id);
        }
    }
}
