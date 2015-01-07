namespace Fifthweek.Api.Identity.OAuth
{
    [AutoConstructor]
    [AutoEqualityMembers]
    public partial class ClientId
    {
        public string Value { get; private set; }
    }
}