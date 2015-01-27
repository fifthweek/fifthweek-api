namespace Fifthweek.Api.Identity.Shared.Membership
{
    using Fifthweek.CodeGeneration;

    [AutoEqualityMembers]
    [AutoConstructor]
    public partial class Password
    {
        public string Value { get; private set; }
    }
}