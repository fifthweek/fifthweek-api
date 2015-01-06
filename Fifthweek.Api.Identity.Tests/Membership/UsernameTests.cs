using System.Collections.Generic;
using Fifthweek.Api.Identity.Membership;

namespace Fifthweek.Api.Identity.Tests.Membership
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class UsernameTests : UsernameTestsBase<Username>
    {
        [TestMethod]
        public void ItShouldAllowLeadingOrTrailingWhitespace()
        {
            this.GoodValue(" joebloggs");
            this.GoodValue("joebloggs ");
        }

        [TestMethod]
        public void ItShouldAllowUppercase()
        {
            this.GoodValue("JoeBloggs");
        }

        protected override Username Parse(string usernameValue)
        {
            return Username.Parse(usernameValue);
        }

        protected override bool TryParse(string usernameValue, out Username username)
        {
            return Username.TryParse(usernameValue, out username);
        }

        protected override bool TryParse(string usernameValue, out Username username, out IReadOnlyCollection<string> errorMessages)
        {
            return Username.TryParse(usernameValue, out username, out errorMessages);
        }

        public static readonly string InvalidValue = "!";
    }
}