namespace Fifthweek.Api.Identity.Membership.Controllers
{
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;

    [AutoEqualityMembers]
    public partial  class RegisterInterestData
    {
        public string Name { get; set; }

        [Parsed(typeof(ValidEmail))]
        public string Email { get; set; }
    }
}