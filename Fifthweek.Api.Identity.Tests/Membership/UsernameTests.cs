namespace Fifthweek.Api.Identity.Tests.Membership
{
    using System.Collections.Generic;

    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Tests.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class UsernameTests : ValidatedStringTests<ValidUsername>
    {
        protected override string ValueA
        {
            get { return "joebloggs"; }
        }

        protected override string ValueB
        {
            get { return "joebloggs2"; }
        }

        [TestMethod]
        public void ItShouldTreatNullAsEmpty()
        {
            var result = ValidUsername.IsEmpty(null);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ItShouldTreatEmptyStringAsEmpty()
        {
            var result = ValidUsername.IsEmpty(string.Empty);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ItShouldTreatStringWithOnlyWhiteSpaceCharactersAsEmpty()
        {
            var result = ValidUsername.IsEmpty(" ");
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ItShouldTreatStringWithAtLeast1NonWhiteSpaceCharacterAsNonEmpty()
        {
            var result = ValidUsername.IsEmpty("a");
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ItShouldAllowBasicUsernames()
        {
            this.GoodValue(this.ValueA);
            this.GoodValue(this.ValueB);
        }

        [TestMethod]
        public void ItShouldNotAllowNull()
        {
            this.BadValue(null);
        }

        [TestMethod]
        public void ItShouldAllowUnderscores()
        {
            this.AssertCharacter('_', isGood: true);
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
            this.AssertMinLength(6, whitespaceSensitive: false);
        }

        [TestMethod]
        public void ItShouldNotAllowUsernamesOver20Characters()
        {
            this.AssertMaxLength(20, whitespaceSensitive: false);
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

        [TestMethod]
        public void ItShouldNormalizeToHaveNoLeadingOrTrailingWhitespace()
        {
            this.GoodNonExactValue(" joebloggs", "joebloggs");
            this.GoodNonExactValue("joebloggs ", "joebloggs");
        }

        [TestMethod]
        public void ItShouldNormalizeToHaveAllLowercase()
        {
            this.GoodNonExactValue("JoeBloggs", "joebloggs");
        }

        protected override ValidUsername Parse(string value)
        {
            return ValidUsername.Parse(value);
        }

        protected override bool TryParse(string value, out ValidUsername parsedObject)
        {
            return ValidUsername.TryParse(value, out parsedObject);
        }

        protected override bool TryParse(string value, out ValidUsername parsedObject, out IReadOnlyCollection<string> errorMessages)
        {
            return ValidUsername.TryParse(value, out parsedObject, out errorMessages);
        }

        protected override string GetValue(ValidUsername parsedObject)
        {
            return parsedObject.Value;
        }
    }
}