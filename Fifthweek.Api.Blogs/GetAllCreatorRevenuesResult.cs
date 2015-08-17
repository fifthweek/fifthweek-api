namespace Fifthweek.Api.Blogs
{
    using System.Collections.Generic;

    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;

    [AutoConstructor, AutoEqualityMembers]
    public partial class GetAllCreatorRevenuesResult
    {
        public IReadOnlyList<Creator> Creators { get; private set; }

        [AutoConstructor, AutoEqualityMembers]
        public partial class Creator
        {
            public UserId UserId { get; private set; }

            public int UnreleasedRevenue { get; private set; }

            public int ReleasedRevenue { get; private set; }

            public int ReleasableRevenue { get; private set; }

            [Optional]
            public Username Username { get; private set; }

            [Optional]
            public CreatorName Name { get; private set; }

            [Optional]
            public Email Email { get; private set; }

            public bool EmailConfirmed { get; private set; }
        }
    }
}