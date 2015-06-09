namespace Fifthweek.Payments.Snapshots
{
    using System;
    using System.Collections.Generic;

    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;

    [AutoEqualityMembers, AutoConstructor]
    public partial class CreatorFreeAccessUsersSnapshot : ISnapshot
    {
        public DateTime Timestamp { get; private set; }

        public UserId CreatorId { get; private set; }

        public IReadOnlyList<string> FreeAccessUserEmails { get; private set; }

        public static CreatorFreeAccessUsersSnapshot Default(DateTime timestamp, UserId creatorId)
        {
            return new CreatorFreeAccessUsersSnapshot(timestamp, creatorId, new List<string>());
        }
    }
}