namespace Fifthweek.Api.Identity.OAuth
{
    [AutoEqualityMembers]
    public partial class ClientId
    {
        public ClientId(string value)
        {
            this.Value = value;
        }

        public string Value { get; private set; }
    }
}