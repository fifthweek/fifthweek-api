namespace Fifthweek.Api.Identity.Membership
{
    using System;

    using Fifthweek.Api.Core;

    public static class Extensions
    {
        public static void AssertAuthenticated(this UserId userId)
        {
            userId.AssertAuthorizedFor(null);
        }

        public static void AssertAuthorizedFor(this UserId userId, UserId requiredUserId)
        {
            if (userId == null)
            {
                throw new UnauthorizedException("The user was not authenticated.");
            }

            if (requiredUserId != null && !requiredUserId.Equals(userId))
            {
                throw new UnauthorizedException(
                    "The user " + requiredUserId.Value.EncodeGuid() + " was required, but user "
                    + userId.Value.EncodeGuid() + " was authenticated.");
            }
        }
    }
}