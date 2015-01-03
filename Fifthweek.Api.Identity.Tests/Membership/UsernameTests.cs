﻿using Fifthweek.Api.Identity.Membership;

namespace Fifthweek.Api.Identity.Tests.Membership
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class UsernameTests : UsernameTestsBase
    {
        [TestMethod]
        public void ItShouldAllowLeadingOrTrailingWhitespace()
        {
            this.GoodUsername(" joebloggs");
            this.GoodUsername("joebloggs ");
        }

        [TestMethod]
        public void ItShouldAllowUppercase()
        {
            this.GoodUsername("JoeBloggs");
        }

        protected override Username Parse(string usernameValue)
        {
            return Username.Parse(usernameValue);
        }

        protected override bool TryParse(string usernameValue, out Username username)
        {
            return Username.TryParse(usernameValue, out username);
        }
    }
}