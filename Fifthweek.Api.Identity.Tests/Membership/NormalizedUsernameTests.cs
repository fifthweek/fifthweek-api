using System.Collections.Generic;
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
            this.BadValue(" joebloggs");
            this.BadValue("joebloggs ");
        }

        [TestMethod]
        public void ItShouldNotAllowUppercase()
        {
            this.BadValue("JoeBloggs");
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

        protected override bool TryParse(string usernameValue, out Username username, out IReadOnlyCollection<string> errorMessages)
        {
            NormalizedUsername normalizedUsername;
            if (NormalizedUsername.TryParse(usernameValue, out normalizedUsername, out errorMessages))
            {
                username = normalizedUsername;
                return true;
            }

            username = null;
            return false;
        }
    }
}