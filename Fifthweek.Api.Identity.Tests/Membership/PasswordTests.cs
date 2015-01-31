namespace Fifthweek.Api.Identity.Tests.Membership
{
    using System.Collections.Generic;

    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Tests.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class PasswordTests : ValidatedStringTests<ValidPassword>
    {
        public static readonly string InvalidValue = "!";

        protected override string ValueA
        {
            get { return "password"; }
        }

        protected override string ValueB
        {
            get { return "password2"; }
        }

        [TestMethod]
        public void ItShouldTreatNullAsEmpty()
        {
            var result = ValidPassword.IsEmpty(null);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ItShouldTreatEmptyStringAsEmpty()
        {
            var result = ValidPassword.IsEmpty(string.Empty);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ItShouldTreatStringWithOnlyWhiteSpaceCharactersAsNonEmpty()
        {
            var result = ValidPassword.IsEmpty(" ");
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ItShouldTreatStringWithAtLeast1NonWhiteSpaceCharacterAsNonEmpty()
        {
            var result = ValidPassword.IsEmpty("a");
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ItShouldAllowBasicPasswords()
        {
            this.GoodValue(this.ValueA);
            this.GoodValue(this.ValueB);
        }

        [TestMethod]
        public void ItShouldNowAllowNull()
        {
            this.BadValue(null);
        }

        [TestMethod]
        public void ItShouldNotAllowPasswordsUnder6Characters()
        {
            this.AssertMinLength(6);
        }

        [TestMethod]
        public void ItShouldNotAllowPasswordsOver100Characters()
        {
            this.AssertMaxLength(100);
        }

        protected override ValidPassword Parse(string value)
        {
            return ValidPassword.Parse(value);
        }

        protected override bool TryParse(string value, out ValidPassword parsedObject)
        {
            return ValidPassword.TryParse(value, out parsedObject);
        }

        protected override bool TryParse(string value, out ValidPassword parsedObject, out IReadOnlyCollection<string> errorMessages)
        {
            return ValidPassword.TryParse(value, out parsedObject, out errorMessages);
        }

        protected override string GetValue(ValidPassword parsedObject)
        {
            return parsedObject.Value;
        }
    }
}