namespace Fifthweek.Api.Identity.Membership
{
    using System;

    using Fifthweek.CodeGeneration;

    [AutoPrimitive]
    public partial class UserId
    {
        public Guid Value { get; private set; }
    }
}