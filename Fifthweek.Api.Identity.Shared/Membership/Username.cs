namespace Fifthweek.Api.Identity.Shared.Membership
{
    using Fifthweek.CodeGeneration;

    [AutoEqualityMembers, AutoJson]
    public partial class Username
    {
        public Username(string value)
        {
            this.Value = Normalize(value);
        }

        public string Value { get; private set; }

        public static string Normalize(string value)
        {
            return value.Trim().ToLower();
        }
    }
}