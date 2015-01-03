using Fifthweek.Api.Identity.Membership;

namespace Fifthweek.Api.Identity.Tests.Membership
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class NormalizedUsernameTests : UsernameTestsBase
    {
        [TestMethod]
        public void ItShouldNotAllowLeadingOrTrailingWhitespace()
        {
            this.BadUsername(" joebloggs");
            this.BadUsername("joebloggs ");
        }

        [TestMethod]
        public void ItShouldNotAllowUppercase()
        {
            this.BadUsername("JoeBloggs");
        }

        protected override Username Parse(string usernameValue)
        {
            return NormalizedUsername.Parse(usernameValue);
        }

        protected override bool TryParse(string usernameValue, out Username username)
        {
            NormalizedUsername normalizedUsername;
            if (NormalizedUsername.TryParse(usernameValue, out normalizedUsername))
            {
                username = normalizedUsername;
                return true;
            }

            username = null;
            return false;
        }
    }
}