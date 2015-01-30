namespace Fifthweek.Api.Identity.Shared.Membership
{
    using Fifthweek.CodeGeneration;

    [AutoEqualityMembers]
    public partial class Email
    {
        public Email(string value)
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