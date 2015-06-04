namespace Fifthweek.Payments
{
    using System;
    using System.Collections.Generic;

    using Fifthweek.CodeGeneration;

    [AutoEqualityMembers, AutoConstructor]
    public partial class CreatorGuestListSnapshot : ISnapshot
    {
        public DateTime Timestamp { get; private set; }

        public Guid CreatorId { get; private set; }

        public IReadOnlyList<string> GuestListEmails { get; private set; }

        public static CreatorGuestListSnapshot Default(DateTime timestamp, Guid creatorId)
        {
            return new CreatorGuestListSnapshot(timestamp, creatorId, new List<string>());
        }
    }
}