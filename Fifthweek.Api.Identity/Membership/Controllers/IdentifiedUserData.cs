namespace Fifthweek.Api.Identity.Membership.Controllers
{
    using Fifthweek.CodeGeneration;

    [AutoEqualityMembers]
    public partial class IdentifiedUserData
    {
        public bool IsUpdate { get; set; }

        public string Name { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }
    }
}