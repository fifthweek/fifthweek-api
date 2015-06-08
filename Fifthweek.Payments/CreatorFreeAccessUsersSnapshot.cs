namespace Fifthweek.Payments
{
    using System;
    using System.Collections.Generic;

    using Fifthweek.CodeGeneration;

    [AutoEqualityMembers, AutoConstructor]
    public partial class CreatorFreeAccessUsersSnapshot : ISnapshot
    {
        public DateTime Timestamp { get; private set; }

        public Guid CreatorId { get; private set; }

        public IReadOnlyList<string> FreeAccessUserEmails { get; private set; }

        public static CreatorFreeAccessUsersSnapshot Default(DateTime timestamp, Guid creatorId)
        {
            return new CreatorFreeAccessUsersSnapshot(timestamp, creatorId, new List<string>());
        }
    }
}