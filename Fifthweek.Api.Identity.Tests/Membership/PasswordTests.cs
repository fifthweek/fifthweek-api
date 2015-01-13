namespace Fifthweek.Api.Identity.Tests.Membership
{
    using System.Collections.Generic;

    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.Api.Tests.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class PasswordTests : ValidatedStringTests<ValidatedPassword>
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

        protected override ValidatedPassword Parse(string value, bool exact)
        {
            return ValidatedPassword.Parse(value);
        }

        protected override bool TryParse(string value, out ValidatedPassword parsedObject, bool exact)
        {
            return ValidatedPassword.TryParse(value, out parsedObject);
        }

        protected override bool TryParse(string value, out ValidatedPassword parsedObject, out IReadOnlyCollection<string> errorMessages, bool exact)
        {
            return ValidatedPassword.TryParse(value, out parsedObject, out errorMessages);
        }

        protected override string GetValue(ValidatedPassword parsedObject)
        {
            return parsedObject.Value;
        }
    }
}