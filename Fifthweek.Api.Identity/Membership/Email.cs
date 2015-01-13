namespace Fifthweek.Api.Identity.Membership
{
    using Fifthweek.Api.Core;

    [AutoEqualityMembers]
    [AutoConstructor]
    public partial class Email
    {
        public string Value { get; private set; }
    }
}