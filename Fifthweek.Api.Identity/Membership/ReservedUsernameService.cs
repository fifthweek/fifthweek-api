namespace Fifthweek.Api.Identity.Membership
{
    using System.Collections.Generic;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Shared;

    public class ReservedUsernameService : IReservedUsernameService
    {
        private readonly HashSet<string> reservedUsernames = new HashSet<string> { "static", "bower_components", "is", "help", "sign-in", "user", "creator" };

        public bool IsReserved(ValidUsername username)
        {
            username.AssertNotNull("username");

            return this.reservedUsernames.Contains(username.Value);
        }

        public void AssertNotReserved(ValidUsername username)
        {
            username.AssertNotNull("username");

            if (this.IsReserved(username))
            {
                throw new RecoverableException("The username '" + username.Value + "' is already taken.");
            }
        }
    }
}