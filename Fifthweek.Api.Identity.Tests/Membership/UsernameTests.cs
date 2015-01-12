using System.Collections.Generic;
using Fifthweek.Api.Identity.Membership;
using Fifthweek.Api.Tests.Shared;

namespace Fifthweek.Api.Identity.Tests.Membership
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class UsernameTests : ValidatedPrimitiveTests<Username, string>
    {
        [TestMethod]
        public void WhenParsingNonExactOnly_ItShouldAllowLeadingOrTrailingWhitespace()
        {
            this.GoodNonExactValue(" joebloggs", "joebloggs");
            this.GoodNonExactValue("joebloggs ", "joebloggs");
        }

        [TestMethod]
        public void WhenParsingNonExactOnly_ItShouldAllowUppercase()
        {
            this.GoodNonExactValue("JoeBloggs", "joebloggs");
        }

        [TestMethod]
        public void ItShouldAllowBasicUsernames()
        {
            this.GoodValue("joebloggs");
        }

        [TestMethod]
        public void ItShouldNotAllowNull()
        {
            this.BadValue(null);
        }

        [TestMethod]
        public void ItShouldAllowUnderscores()
        {
            this.GoodValue("joe_bloggs");
        }

        [TestMethod]
        public void ItShouldAllowNumbers()
        {
            this.GoodValue("joe123");
            this.GoodValue("123456");
        }

        [TestMethod]
        public void ItShouldNotAllowUsernamesUnder6Characters()
        {
            this.GoodValue("joeblo");
            this.BadValue("joebl");
        }

        [TestMethod]
        public void ItShouldNotAllowUsernamesOver20Characters()
        {
            this.GoodValue("joe_bloggs_123456789");
            this.BadValue("joe_bloggs_1234567890");
        }

        [TestMethod]
        public void ItShouldNotAllowIllegalCharacters()
        {
            // Include 'a' to ensure we're not just checking for the existence of at least one valid character.
            this.BadValue("a!@£#$%^&*()-+={}[]");
            this.BadValue("a:;'`\"\\|<>,./?~§±");
        }

        [TestMethod]
        public void ItShouldNotAllowInnerWhitespace()
        {
            this.BadValue("joe bloggs");
            this.BadValue("joe\nbloggs");
        }

        protected override string ValueA
        {
            get { return "joe_bloggs"; }
        }

        protected override string ValueB
        {
            get { return "joe_bloggs2"; }
        }

        protected override Username Parse(string value, bool exact)
        {
            return Username.Parse(value, exact);
        }

        protected override bool TryParse(string value, out Username parsedObject, bool exact)
        {
            return Username.TryParse(value, out parsedObject, exact);
        }

        protected override bool TryParse(string value, out Username parsedObject, out IReadOnlyCollection<string> errorMessages, bool exact)
        {
            return Username.TryParse(value, out parsedObject, out errorMessages, exact);
        }

        protected override string GetValue(Username parsedObject)
        {
            return parsedObject.Value;
        }
    }
}