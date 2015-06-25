namespace Fifthweek.Payments.Services
{
    using System;

    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;

    [AutoConstructor, AutoEqualityMembers]
    public partial class CreatorIdAndFirstSubscribedDate
    {
        public UserId CreatorId { get; private set; }

        public DateTime FirstSubscribedDate { get; private set; }
    }
}