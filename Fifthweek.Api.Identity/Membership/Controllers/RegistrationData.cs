namespace Fifthweek.Api.Identity.Membership.Controllers
{
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;

    [AutoEqualityMembers]
    public partial class RegistrationData
    {
        [Optional]
        public string ExampleWork { get; set; }

        [Parsed(typeof(ValidEmail))]
        public string Email { get; set; }

        [Parsed(typeof(ValidUsername))]
        public string Username { get; set; }

        [Parsed(typeof(ValidPassword))]
        public string Password { get; set; }
    }
}