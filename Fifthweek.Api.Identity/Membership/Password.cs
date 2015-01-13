namespace Fifthweek.Api.Identity.Membership
{
    using Fifthweek.Api.Core;

    [AutoEqualityMembers]
    [AutoConstructor]
    public partial class Password
    {
        public string Value { get; private set; }
    }
}