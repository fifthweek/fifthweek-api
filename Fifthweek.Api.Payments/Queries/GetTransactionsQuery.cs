namespace Fifthweek.Api.Payments.Queries
{
    using System;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Payments.Services.Administration;

    [AutoConstructor, AutoEqualityMembers]
    public partial class GetTransactionsQuery : IQuery<GetTransactionsResult>
    {
        public Requester Requester { get; private set; }

        [Optional]
        public UserId UserId { get; private set; }

        [Optional]
        public DateTime? StartTimeInclusive { get; private set; }

        [Optional]
        public DateTime? EndTimeExclusive { get; private set; }
    }
}