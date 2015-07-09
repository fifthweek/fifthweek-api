namespace Fifthweek.Api.Payments.Queries
{
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Payments.Controllers;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;

    [AutoConstructor, AutoEqualityMembers]
    public partial class GetCreditRequestSummaryQuery : IQuery<CreditRequestSummary>
    {
        public Requester Requester { get; private set; }

        public UserId UserId { get; private set; }
    }
}