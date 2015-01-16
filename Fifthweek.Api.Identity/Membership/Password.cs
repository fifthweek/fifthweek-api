namespace Fifthweek.Api.Identity.Membership
{
    using Fifthweek.Api.Core;
    using Fifthweek.Shared;

    [AutoEqualityMembers]
    [AutoConstructor]
    public partial class Password
    {
        public string Value { get; private set; }
    }
}