using Fifthweek.Api.Identity.Membership;
using Fifthweek.Api.Tests.Shared;

namespace Fifthweek.Api.Identity.Tests.Membership
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public abstract class UsernameTestsBase<T> : ValidatedPrimitiveEqualityTests<T, string> where T : Username
    {
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

        protected override string GetValue(T parsedObject)
        {
            return parsedObject.Value;
        }
    }
}